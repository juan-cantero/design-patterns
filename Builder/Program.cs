using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    class HtmlElemet
    {
        public string name, text;
        public List<HtmlElemet> elements = new List<HtmlElemet>();
        private const int IndentSize = 2;

        public HtmlElemet()
        {

        }

        public HtmlElemet(string name, string text)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.text = text ?? throw new ArgumentNullException(nameof(text));
        }

        private string ToStringImplementation(int indent)
        {
            var sb = new StringBuilder();
            var indentation = new String(' ',IndentSize * indent);
            sb.AppendLine($"{indentation}<{name}>");

            if(!string.IsNullOrWhiteSpace(text))
            {
                sb.Append(new String(' ',IndentSize * (indent+1)));
                sb.AppendLine(text);
            }

            foreach (var e in elements)
            {
                sb.Append(e.ToStringImplementation(indent + 1));
            }
            sb.AppendLine($"{indentation}</{name}>");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImplementation(0);
        }
    }

    class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElemet root = new HtmlElemet();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName ?? throw new ArgumentNullException(nameof(rootName));
            this.root.name = rootName;
        }

        public void AddChild(string childName,string childText)
        {
            this.root.elements.Add(new HtmlElemet(childName,childText));

        }

        public override string ToString()
        {
            return root.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append("hola");
            sb.Append("</p>");
            Console.WriteLine(value: sb);

            var htmlBuilder = new HtmlBuilder("ul");
            htmlBuilder.AddChild("li","name");
            Console.WriteLine(htmlBuilder);

        }
    }
}
