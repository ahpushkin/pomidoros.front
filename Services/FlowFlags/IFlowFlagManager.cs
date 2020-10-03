namespace Services.FlowFlags
{
    public interface IFlowFlagManager
    {
        bool Is(string key);
        
        void Set(string key, bool val);
    }
}