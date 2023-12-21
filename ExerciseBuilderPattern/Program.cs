using System.Text;
using System.Collections.Generic;

namespace ExerciseBuilderPattern
{
    internal class Program
    {
        public class Property
        {
            public string PropertyName { get; set; }
            public string PropertyType { get; set; }

            public Property(string propertyName, string propertyType)
            {
                this.PropertyName = propertyName;
                this.PropertyType = propertyType;
            }
        }
        public class CodeBuilder
        {
            private int Indent = 2;
            private string className;
            private readonly List<Property> properties = new List<Property>();
            public CodeBuilder(string className)
            {
                this.className = className;
            }

            public CodeBuilder AddField(string name, string type)
            {
                var property = new Property(name, type);
                properties.Add(property);
                return this;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                sb.AppendLine(string.Format("public class {0}", className));
                sb.AppendLine("{");
                foreach (var prop in properties)
                {
                    sb.Append(' ', Indent);
                    sb.AppendLine(string.Format("public {0} {1};", prop.PropertyType, prop.PropertyName));
                }
                sb.AppendLine("}");

                return sb.ToString();
            }
        }
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}