using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Interfaces
{
    using Models;

    public interface ISizeGroupRepos
    {
        void Create(SizeGroup group);
        void Read();
        void Update(string refId, SizeGroup group);
        void Delete(string groupId);
    }
}