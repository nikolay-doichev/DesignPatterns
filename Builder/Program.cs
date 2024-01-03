using System.Text;

namespace Builder;

class HtmlBuilder
{
    private readonly string rootName;
    private HtmlElement root = new HtmlElement();

    public HtmlBuilder(string rootName)
    {
        this.rootName = this.rootName;
        this.root.Name = rootName;
    }

    public HtmlBuilder AddChild(string childName, string childText)
    {
        var e = new HtmlElement(childName, childText);
        root.Elements.Add(e);
        return this;
    }

    public override string ToString()
    {
        return root.ToString();
    }

    public void Clear()
    {
        root = new HtmlElement { Name = rootName };
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        var hello = "hello";
        var sb = new StringBuilder();
        sb.Append("<p>");
        sb.Append(hello);
        sb.Append("</p>");

        Console.WriteLine(sb);

        var words = new[] { "hello", "word" };
        sb.Clear();
        sb.Append("<ul>");
        foreach (var word in words)
        {
            sb.AppendFormat("<li>{0}</li>", word);
        }
        sb.Append("</ul>");
        Console.WriteLine(sb);

        var builder = new HtmlBuilder("ul");
        builder.AddChild("li", "hello").AddChild("li", "world");
        Console.WriteLine(builder.ToString());
    }
}