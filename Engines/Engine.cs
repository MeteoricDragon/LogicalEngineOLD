﻿using LogicalEngine.EngineContainers;
using LogicalEngine.EngineParts;
using LogicalEngine.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalEngine
{
    public abstract class Engine
    {
        public int CycleCount = 0;
        public abstract bool CycleComplete { get; }
        protected EngineOperationOrder EngineOrder;
        public List<EngineSubsystem> Subsystems { get; protected set; }

        public List<CarPart> AllParts
        {
            get {
                var parts = new List<CarPart>();
                foreach (var x in Subsystems)
                {
                    parts.AddRange(x.Parts);
                }
                return parts; 
            }
        }

        public virtual void RunEngine()
        {
            while (CycleCount++ < 1) /* enginecycles < 0 */
            // run engine while ignitionswitch is set, but it's not on this hierarchy
            {
                TickEngine();
                // don't need to reset cyclecomplete, it is get only
            }
                
                ;
        }
        public virtual void TickEngine()
        {
            
        }
        public virtual void StartEngine()
        {

        }
        public virtual void StopEngine()
        {

        }

        public Engine()
        {
            Subsystems = new List<EngineSubsystem>();
            EngineOrder = new EngineOperationOrder();
        }

        protected void AssembleEngine() 
        { 
            foreach (EngineSubsystem s in Subsystems)
            {
                foreach (CarPart p in s.Parts)
                {
                    AssignPartListToPart(p);
                }
            }
        }

        protected abstract void AssignPartListToPart(CarPart p);
    }
}