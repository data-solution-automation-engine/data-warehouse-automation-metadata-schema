using Newtonsoft.Json;

var inputMetadataDirectory = @"D:\Git_Repos\Projects\jarvis-TEAM-metadata\Development\TEAM\Metadata";
var outputMetadataDirectory = @"D:\Git_Repos\";

var exceptionList = new List<string>
{
    "VDW_Samples_TEAM_Attribute_Mapping.json",
    "Development_TEAM_Attribute_Mapping.json",
    "sample_TEAM_Attribute_Mapping.json"
};

List<DataItemMappingTuple> exportOutput = new();

foreach (string file in Directory.EnumerateFiles(inputMetadataDirectory, "*.json", SearchOption.TopDirectoryOnly))
{
    if (!exceptionList.Contains(Path.GetFileName(file)))
    {
        try
        {
            Console.WriteLine(file);
            var json = File.ReadAllText(file);
            var deserializedMapping = JsonConvert.DeserializeObject<DataWarehouseAutomation.DataObjectMappingList>(json);

            foreach (var dataObjectMapping in deserializedMapping.DataObjectMappings)
            {
                var sourceDataObjectRaw = dataObjectMapping.SourceDataObjects.FirstOrDefault();

                if (dataObjectMapping.DataItemMappings != null)
                {
                    foreach (var dataItemMapping in dataObjectMapping.DataItemMappings)
                    {
                        var sourceDataItemRaw = dataItemMapping.SourceDataItems.FirstOrDefault();

                        var localDataItemMappingTuple = new DataItemMappingTuple
                        {
                            SourceDataObject = sourceDataObjectRaw.name,
                            SourceDataItem = sourceDataItemRaw.name,
                            TargetDataObject = dataObjectMapping.TargetDataObject.Name,
                            TargetDataItem = dataItemMapping.TargetDataItem.Name
                        };

                        if (!exportOutput.Contains(localDataItemMappingTuple))
                        {
                            exportOutput.Add(localDataItemMappingTuple);
                        }
                    }
                }
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Issue: " + exception.Message);
        }
    }
    else
    {
        Console.WriteLine($"Skipping " + Path.GetFileName(file));
    }
}

// Export to file.
using (StreamWriter writer = new StreamWriter(outputMetadataDirectory + "TEAM-export.csv"))
{
    foreach (var exportRow in exportOutput)
    {
        try
        {
            writer.WriteLine($"{exportRow.SourceDataObject},{exportRow.SourceDataItem},{exportRow.TargetDataObject},{exportRow.TargetDataItem}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Issue: " + exception.Message);
        }
    }
}

// Finish the application.
Console.WriteLine("Done, press any key to exit");
Console.ReadKey();

internal class DataItemMappingTuple
{
    internal string SourceDataObject
    {
        get;
        set;
    } = string.Empty;

    internal string SourceDataItem
    {
        get;
        set;
    } = string.Empty;

    internal string TargetDataObject
    {
        get;
        set;
    } = string.Empty;

    internal string TargetDataItem
    {
        get;
        set;
    } = string.Empty;
}