using RimWorld;
using Verse;
using Verse.AI;

namespace ElectromagneticPulseGenerator
{
    public class WorkGiver_ScanForOreDeposit : WorkGiver_Scanner
    {
        public override PathEndMode PathEndMode => PathEndMode.InteractionCell;
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(DefDatabase<ThingDef>.GetNamed("ElectromagneticPulseGenerator"));
        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            var epg = t as Building_ElectromagneticPulseGenerator;
            if (epg == null || epg.PowerComp == null || !epg.PowerComp.PowerOn)
                return null;
            if (epg.UnrevealedDeposits.Count == 0)
                return null;
            return JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("ScanForOreDeposit"), t);
        }
    }
}
