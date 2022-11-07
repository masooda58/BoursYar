using System.Collections.Generic;
using Common.FrameWork.Domain.Events;

namespace Common.FrameWork.Domain.Data
{
    public interface IAggregateStore
    {
        //bool Exist<T, TId>(TId aggregateId);
        //Task Save<T, TId>(T aggregate) where T : BaseAggregateRoot<TId>;
        //Task<T> Load<T,TId>(TId aggregateId)where T : BaseAggregateRoot<TId>;
    }
   public interface IEventSource
   {
       void Save<TEvent>(string aggregateName, string streamId, IEnumerable<TEvent> events) where TEvent : IEvent;

   }
}
