﻿using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine.EngineParts
{
    public class IgnitionCoil : ElectricalPart
    {
        public override string UserFriendlyName { get => "Ignition Coil"; }
        public IgnitionCoil(Engine e) : base(e)
        {
            CanDrawFromBattery = true;
        }
        protected override bool ThresholdTriggered(CarPart activatingPart)
        {
            if ((activatingPart as IgnitionSwitch).IgnitionSwitchOn)
                return base.ThresholdTriggered(activatingPart);
            return false;
        }
    }
}