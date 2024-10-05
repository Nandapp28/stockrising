public class SemesterStateFactory
{
    SemesterStateMachine _context;
    public SemesterStateFactory(SemesterStateMachine currentContext)
    {
        _context = currentContext;
    }

    public FirstSemesterState FirstSemester()
    {
        return new FirstSemesterState(_context, this);
    }
    public SecondSemesterState SecondSemester()
    {
        return new SecondSemesterState(_context, this);
    }
    public BiddingPhaseState BiddingPhase()
    {
        return new BiddingPhaseState(_context, this);
    }
    public ActionPhaseState ActionPhase()
    {
        return new ActionPhaseState(_context, this);
    }
    //public SalesPhaseState SalesPhase()
    //{
    //    return new SalesPhaseState();
    //}
    //public RumorPhaseState RumorPhase()
    //{
    //    return new RumorPhaseState();
    //}
    //public ResolutionPhaseState ResolutionPhase()
    //{
    //    return new ResolutionPhaseState();
    //}
}
