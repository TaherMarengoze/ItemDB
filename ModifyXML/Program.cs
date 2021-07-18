using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModifyXML
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = @"G:\Taher\R&D\ItemCatalog\Debug\Sample Data\Specs.xml";
            string outputFilePath = @"G:\Taher\R&D\ItemCatalog\Debug\Sample Data\Specs_mod.xml";
            XDocument xFile = XDocument.Load(inputFilePath);
            //==========================================================

            foreach (var item in xFile.Descendants("entry"))
            {
                XElement val = item.Element("val");
                XElement display = item.Element("disp");

                if (display == null)
                {
                    item.Add(new XElement("disp") { Value = val.Value });
                }
            }

            //==========================================================
            Console.WriteLine(xFile.Document.ToString());
            Console.WriteLine("\nPress ENTER to save the modified file.");
            Console.ReadLine();
            xFile.Save(outputFilePath);

            
        }

        static void ModifyItem1()
        {
            //foreach (XElement item in xFile.Descendants("entry"))
            //{
            //    if (!item.HasElements)
            //    {
            //        string itemValue = item.Value;
            //        item.Value = string.Empty;
            //        item.Add(
            //            new XElement("disp") { Value = itemValue },
            //            new XElement("val") { Value = itemValue }
            //            );
            //        Console.WriteLine(item.ToString());
            //    }

            //}
        }

        static void ModifyItemDetails()
        {
            //foreach (XElement item in xFile.Descendants("details"))
            //{
            //    string specs = item.Attribute("specsID").Value;
            //    string sizeGroup = item.Attribute("sizeGroupID").Value;
            //    string brandList = item.Attribute("brandListID").Value;
            //    string endsList = item.Attribute("endsListID").Value;

            //    string specsReq = item.Element("required").Attribute("specs").Value;
            //    string sizeGroupReq = item.Element("required").Attribute("size").Value;
            //    string brandListReq = item.Element("required").Attribute("brand").Value;
            //    string endsListReq = item.Element("required").Attribute("ends").Value;

            //    item.RemoveAttributes();
            //    item.RemoveNodes();

            //    item.Add(
            //        new XElement("specs", new XAttribute("ID", specs), new XAttribute("required", specsReq)),
            //        new XElement("sizeGroup", new XAttribute("ID", sizeGroup), new XAttribute("required", sizeGroupReq)),
            //        new XElement("brandList", new XAttribute("ID", brandList), new XAttribute("required", brandListReq)),
            //        new XElement("endsList", new XAttribute("ID", endsList), new XAttribute("required", endsListReq)));


            //    Console.WriteLine(item.ToString());
            //}
        }
    }
}