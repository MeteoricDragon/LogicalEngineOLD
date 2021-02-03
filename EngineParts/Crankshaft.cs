﻿using LogicalEngine.EngineContainers;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using static LogicalEngine.EngineParts.CombustionChambers;

namespace LogicalEngine.EngineParts
{
    public class Crankshaft : MechanicalPart
    {
        public override string UserFriendlyName { get => "Crankshaft"; }

        public Crankshaft(Engine e) : base(e)
        {
            Engine = e;
            UnitsOwned = 5;
            FrictionResistance = 0;
        }

        protected override bool ShouldActivate(CarPart target, in bool transferSuccess, in bool didAdjustment)
        {
            if (Engine.CycleComplete)
                return false;

            // TODO: activate Flywheel if we're going to torque converter (not yet)
            return true;
        }

        protected override bool BackToEngineLoop(CarPart sender)
        {// TODO: if doing Torque Converter, readdress how the engine 
            //goes back to loop? Will torque converter be triggered by other strokes?
            if (sender is Pistons && Engine.CycleComplete) 
                return true;
            return false;
        }

        public void Tick()
        {
            InvokeActivate();
        }
    }
}