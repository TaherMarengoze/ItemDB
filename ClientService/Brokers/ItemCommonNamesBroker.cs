using Interfaces.Operations;
using System.Collections.Generic;

namespace ClientService.Brokers
{
    public interface ISourceModifiable
    {
        void ModiftSource(List<string> newRepos);
    }

    public class ItemCommonNamesBroker : IBroker<string>, ISourceModifiable
    {
        private readonly List<string> repos;

        public ItemCommonNamesBroker(List<string> repos)
        {
            this.repos = repos;
        }

        public void Create(string content)
        {
            repos.Add(content);
        }

        public string Read(string entityId)
        {
            return repos.Find(entry => entry == entityId);
        }

        public void Update(string refId, string content)
        {
            int index = repos.IndexOf(refId);
            repos[index] = content;
        }

        public void Delete(string entityId)
        {
            repos.Remove(entityId);
        }

        public void ModiftSource(List<string> newRepos)
        {

        }
    }
}