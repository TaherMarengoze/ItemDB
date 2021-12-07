using AppCore;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService
{
    /// <summary>
    /// Provides static methods for getting field data from entities and also updates those data when models' data change.
    /// </summary>
    /*public static class DataProvider
    {
        internal static void RegisterModelListsEvents()
        {
            Globals.ModelLists.OnSpecsChanged += Lists_OnSpecsChanged;
            Globals.ModelLists.OnSizeGroupsChanged += ModelLists_OnSizeGroupsChanged;
            Globals.ModelLists.OnSizeListChanged += ModelLists_OnSizeListChanged;
            Globals.ModelLists.OnCustomSizeListChanged += ModelLists_OnCustomSizeListChanged;
        }
        
        private static void Lists_OnSpecsChanged(object sender, EventArgs e)
        {
            Globals.DataLists.SpecsIDs =
                Globals.ModelLists.Specs.Select(entity => entity.ID).ToList();
        }

        private static void ModelLists_OnSizeGroupsChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Globals.DataLists.SizeGroupIDs =
                Globals.ModelLists.SizeGroups.Select(entity => entity.ID).ToList();
        }

        private static void ModelLists_OnSizeListChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Globals.DataLists.SizeIDs =
                Globals.ModelLists.SizeLists.Select(size => size.ID).ToList();
        }

        private static void ModelLists_OnCustomSizeListChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Globals.DataLists.CustomSizeIDs =
                Globals.ModelLists.CustomSizes;
        }

        /// <summary>
        /// Gets a list of ID for all specs objects.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIDs()
        {
            return Globals.DataLists.SpecsIDs;
        }
        public static List<string> FilterSpecsIds(string exactId)
        {
            return
                Globals.DataLists.SpecsIDs
                .Where(id => id.Contains(exactId))
                .ToList();
        }
        public static int SpecsCount => GetIDs().Count;

        public static class Specs
        {
            public static List<string> GetIDs() =>
                Globals.DataLists.SpecsIDs;

            public static List<string> FilterSpecsIds(string exactId) =>
                Globals.DataLists.SpecsIDs
                    .Where(id => id.Contains(exactId))
                    .ToList();

            public static int SpecsCount => GetIDs().Count;
        }

        public static class SizeGroup
        {
            public static List<ISizeGroup> GetList()
            {
                // .ToList will return a copy preserving the one in the cache
                return CacheIO.GetSizeGroupList().ToList();
            }

            /// <summary>
            /// Gets a list of ID for SizeGroup objects.
            /// </summary>
            /// <returns></returns>
            public static List<string> GetIDs()
            {
                return Globals.DataLists.SizeGroupIDs;
            }
        }

        public static class Size
        {
            public static List<string> GetIDs()
            {
                return Globals.DataLists.SizeIDs;
            }
        }

        public static class CustomSize
        {
            public static List<string> GetIDs()
            {
                return Globals.DataLists.CustomSizeIDs;
            }
        }
    }
    */
    public class DataProvider
    {
        public DataProvider()
        {
            Globals.ModelLists.OnSpecsChanged += Lists_OnSpecsChanged;
            Globals.ModelLists.OnSizeGroupsChanged += ModelLists_OnSizeGroupsChanged;
            Globals.ModelLists.OnSizeListChanged += ModelLists_OnSizeListChanged;
            Globals.ModelLists.OnCustomSizeListChanged += ModelLists_OnCustomSizeListChanged;
        }

        private void Lists_OnSpecsChanged(object sender, EventArgs e)
        {
            Globals.DataLists.SpecsIDs =
                Globals.ModelLists.Specs.Select(entity => entity.ID).ToList();
        }

        private void ModelLists_OnSizeGroupsChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Globals.DataLists.SizeGroupIDs =
                Globals.ModelLists.SizeGroups.Select(entity => entity.ID).ToList();
        }

        private void ModelLists_OnSizeListChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Globals.DataLists.SizeIDs =
                Globals.ModelLists.SizeLists.Select(size => size.ID).ToList();
        }

        private void ModelLists_OnCustomSizeListChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Globals.DataLists.CustomSizeIDs =
                Globals.ModelLists.CustomSizes;
        }

        /// <summary>
        /// Gets a list of ID for all specs objects.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIDs()
        {
            return Globals.DataLists.SpecsIDs;
        }
        public static List<string> FilterSpecsIds(string exactId)
        {
            return
                Globals.DataLists.SpecsIDs
                .Where(id => id.Contains(exactId))
                .ToList();
        }
        public static int SpecsCount => GetIDs().Count;

        public class Specs
        {
            public List<string> GetIDs() =>
                Globals.DataLists.SpecsIDs;

            public List<string> FilterSpecsIds(string exactId) =>
                Globals.DataLists.SpecsIDs
                    .Where(id => id.Contains(exactId))
                    .ToList();

            public int SpecsCount => GetIDs().Count;
        }

        public class SizeGroup : IProvider<ISizeGroup>
        {
            public List<ISizeGroup> GetList()
            {
                return CacheIO.GetSizeGroupList();
            }

            /// <summary>
            /// Gets a list of ID for SizeGroup objects.
            /// </summary>
            /// <returns></returns>
            public List<string> GetIDs()
            {
                return Globals.DataLists.SizeGroupIDs;
            }
        }

        public class Size : IProvider<IFieldList>
        {
            public List<IFieldList> GetList() => throw new NotImplementedException();

            public List<string> GetIDs() => Globals.DataLists.SizeIDs;

            public List<string> GetEntries(string listID)
            {
                System.Collections.ObjectModel.ObservableCollection<string> entries =
                    (from list in CacheIO.GetSizeList()
                     where list.ID == listID
                     select list.List).FirstOrDefault();

                return entries?.ToList();
            }
        }

        public class CustomSize
        {
            public List<string> GetIDs()
            {
                return Globals.DataLists.CustomSizeIDs;
            }
        }
    }

    public interface IProvider<T>
    {
        List<T> GetList();

        List<string> GetIDs();
    }
}
