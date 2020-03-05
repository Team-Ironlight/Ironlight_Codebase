// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
namespace UPool
{
    public interface IGenerator
    {
        IPoolable CreateInstance();
    }
}
