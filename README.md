# SQLGenerator
This file will generate sql table from C# class file.

# Example

```csharp
public void Example()
        {
            Console.WriteLine(new SQLGenerator(new WeatherForecast().GetType()).CreateTableScript());
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }
        }
        
```
# Fiddle
You can play with it [here](https://dotnetfiddle.net/HXleWq)
