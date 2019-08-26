public class SQLGenerator
{
    public SQLGenerator(Type t)
    {
        ClassName = t.Name;

        foreach (var p in t.GetProperties())
        {
            var field = new KeyValuePair<string, Type>(p.Name, p.PropertyType);

            Fields.Add(field);
        }
    }

    private Dictionary<Type, string> dataMapper
    {
        get
        {
            // Add the rest of your CLR Types to SQL Types mapping here
            var dataMapper = new Dictionary<Type, string>();
            dataMapper.Add(typeof(int), "BIGINT");
            dataMapper.Add(typeof(string), "NVARCHAR(500)");
            dataMapper.Add(typeof(bool), "BIT");
            dataMapper.Add(typeof(DateTime), "DATETIME");
            dataMapper.Add(typeof(float), "FLOAT");
            dataMapper.Add(typeof(decimal), "DECIMAL(18,0)");
            dataMapper.Add(typeof(Guid), "UNIQUEIDENTIFIER");

            return dataMapper;
        }
    }

    public List<KeyValuePair<string, Type>> Fields { get; set; } = new List<KeyValuePair<string, Type>>();

    public string ClassName { get; set; } = string.Empty;

    public string CreateTableScript()
    {
        var script = new StringBuilder();

        script.AppendLine("CREATE TABLE " + ClassName);
        script.AppendLine("(");
        script.AppendLine("\t ID BIGINT,");
        for (var i = 0; i < Fields.Count; i++)
        {
            var field = Fields[i];

            if (dataMapper.ContainsKey(field.Value))
                script.Append("\t " + field.Key + " " + dataMapper[field.Value]);
            else
                // Complex Type? 
                script.Append("\t " + field.Key + " BIGINT");

            if (i != Fields.Count - 1) script.Append(",");

            script.Append(Environment.NewLine);
        }

        script.AppendLine(")");

        return script.ToString();
    }
}