// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
namespace UPool
{
    public interface IPoolable
    {
        void Init(AbstractPool owner);
        void OnAllocate();
        void OnDeallocate();
        void Destroy();
    }
}