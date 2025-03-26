using BetsAppMVC.Models;
using Microsoft.AspNetCore.SignalR;

namespace BetsAppMVC.Services
{
    public interface IEventsService
    {
        List<Event> GetEvents();
        Event GetEventById(int id);
    }
    public class EventsService : IEventsService
    {
        private List<Event> _events;
        private string fileName = "events.json";
        public EventsService() 
        {
            _events = JsonHelper.ReadJson<Event>(fileName);
        }
        public List<Event> GetEvents() 
        { 
            return _events; 
        }   

        public Event GetEventById(int id)
        {
            return _events.FirstOrDefault(it => it.Id == id);
        }
    }
}
