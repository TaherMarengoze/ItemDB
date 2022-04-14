using System.Collections.Generic;
using Interfaces.Operations;

namespace ClientService.Brokers
{
    public class ItemImageNamesBroker : IBroker<string>, ISourceModifiable
    {
        private readonly List<string> repos;

        public ItemImageNamesBroker(List<string> repos)
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