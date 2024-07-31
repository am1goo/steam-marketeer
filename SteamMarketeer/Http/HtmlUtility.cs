using HtmlAgilityPack;
using System.Collections.Generic;

public static class HtmlUtility
{
    public static HtmlNode GetDivWithAttribute(HtmlDocument document, string attrName, string attrValue)
    {
        return GetDivWithAttribute(document.DocumentNode, new Attribute(attrName, attrValue));
    }

    public static HtmlNode GetDivWithAttribute(HtmlDocument document, params Attribute[] attributes)
    {
        return GetDivWithAttribute(document.DocumentNode, attributes);
    }

    public static HtmlNode GetDivWithAttribute(HtmlNode node, string attrName, string attrValue)
    {
        return GetDivWithAttribute(node, new Attribute(attrName, attrValue));
    }

    public static HtmlNode GetDivWithAttribute(HtmlNode node, params Attribute[] attributes)
    {
        var result = new List<HtmlNode>();
        GetDivWithAttribute(node, result, attributes);
        return result.Count > 0 ? result[0] : null;
    }

    public static void GetDivWithAttribute(HtmlDocument document, List<HtmlNode> result, string attrName, string attrValue)
    {
        GetDivWithAttribute(document.DocumentNode, result, new Attribute(attrName, attrValue));
    }

    public static void GetDivWithAttribute(HtmlDocument document, List<HtmlNode> result, params Attribute[] attributes)
    {
        GetDivWithAttribute(document.DocumentNode, result, attributes);
    }

    public static void GetDivWithAttribute(HtmlNode node, List<HtmlNode> result, string attrName, string attrValue)
    {
        GetDivWithAttribute(node, result, new Attribute(attrName, attrValue));
    }

    public static void GetDivWithAttribute(HtmlNode node, List<HtmlNode> result, params Attribute[] attributes)
    {
        var children = node.Descendants();
        foreach (var child in children)
        {
            if (child.Name != "div")
                continue;

            var skip = false;
            foreach (var attr in attributes) 
            {
                var attrValue = child.GetAttributeValue(attr.name, null);
                if (attrValue == null)
                {
                    skip = true;
                    break;
                }

                if (attrValue != attr.value)
                {
                    skip = true;
                    break;
                }
            }

            if (skip)
                continue;
            
            result.Add(child);
        }
    }

    public struct Attribute
    {
        public string name;
        public string value;

        public Attribute(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
