﻿using LogicalEngine;
using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.Engines.CombustionEngine;

namespace LogicalEngine.EngineParts
{
    public class CombustionChamber : FuelPart
    {
        public enum CombustionStrokeCycle
        {
            Intake,
            Compression,
            Combustion,
            Exhaust
        };
        public CombustionStrokeCycle StrokeCycle { get; protected set; }
        public override string UserFriendlyName { get => "Combustion Chamber"; }
        public CombustionChamber(Engine e) : base(e)
        {
            Engine = e;
        }

        protected override bool ThresholdTriggered(CarPart activatingPart)
        {
            if (StrokeCycle == CombustionStrokeCycle.Combustion)
                Fill(UnitsToGive);
            
            return base.ThresholdTriggered(activatingPart);
        }

        private void StrokeCycleChange()
        {
            var cycle = StrokeCycle;

            switch (cycle)
            {
                case CombustionStrokeCycle.Exhaust:
                    cycle = CombustionStrokeCycle.Intake;
                    break;
                case CombustionStrokeCycle.Intake:
                    cycle = CombustionStrokeCycle.Compression;
                    break;
                case CombustionStrokeCycle.Compression:
                    cycle = CombustionStrokeCycle.Combustion;
                    break;
                case CombustionStrokeCycle.Combustion:
                    cycle = CombustionStrokeCycle.Exhaust;
                    break;
            }

            if (StrokeCycle != cycle)
            {
                StrokeCycle = cycle;
                Output.ChangeCycleReport(cycle);
            }
        }

        protected override void AdjustFlow()
        {
            StrokeCycleChange();
            // TODO: prevent cycle change If activating part was spark plug unless combustion to exhaust
            // TODO: prevent cycle change if activating part wasn't spark plug and is any other change other than combustion to exhaust
        }
    }
}
