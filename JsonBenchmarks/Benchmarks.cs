using BenchmarkDotNet.Attributes;
using Bogus;
using Newtonsoft.Json;

namespace JsonBenchmarks;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByCategory)]
[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net80)]
[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net90)]
[CategoriesColumn]
public class Benchmarks
{
    private List<Person> _people = new List<Person>();

    [GlobalSetup]
    public void Setup()
    {
        // Create a Faker instance with the desired locale
        var faker = new Faker<Person>();

        // Generate a list of fake persons
        _people = faker
            .RuleFor(p => p.ID, f => f.IndexFaker)
            .RuleFor(p => p.FirstName, f => f.Person.FirstName)
            .RuleFor(p => p.LastName, f => f.Person.LastName)
            .Generate(500); // You can change this number to generate more or fewer persons
    }

    [Benchmark]
    public string NewtonSoft_Serialization()
    {
        return JsonConvert.SerializeObject(_people);
    }

    [Benchmark]
    public string BCL_Serialization()
    {
        return System.Text.Json.JsonSerializer.Serialize(_people);
    }
}
