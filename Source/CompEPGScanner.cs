using RimWorld;
using Verse;
using Verse.AI;
using System.Collections.Generic;

namespace ElectromagneticPulseGenerator
{
    public class CompEPGScanner : ThingComp
    {
        public CompProperties_EPGScanner Props => (CompProperties_EPGScanner)props;
        private int scanTicks = 0;
        private Building_ElectromagneticPulseGenerator EPG => parent as Building_ElectromagneticPulseGenerator;

        public float GetScanProgress()
        {
            return (float)scanTicks / (float)Props.maxScanTimeTicks;
        }

        public override void CompTickRare()
        {
            base.CompTickRare();
            // Only add automatic progress if powered
            var power = parent.TryGetComp<CompPowerTrader>();
            if (power != null && power.PowerOn)
            {
                AddProgress(250);
            }
        }

        public void AddProgress(int amount)
        {
            scanTicks += amount;
            // MTB logic: chance to find each call
            float mtb = Props.meanTimeBetweenFindsDays * 60000f; // convert days to ticks
            if (Rand.MTBEventOccurs(mtb, 60000f, amount) || scanTicks >= Props.maxScanTimeTicks)
            {
                scanTicks = 0;
                EPG?.RevealRandomOreDeposit(EPG.Map);
            }
        }
    }
}
