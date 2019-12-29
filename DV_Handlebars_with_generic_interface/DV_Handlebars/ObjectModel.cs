using System.Collections.Generic;

class MappingList
{
    public List<IndividualMetadataMapping> sourceToTargetMappingList { get; set; }
}

class IndividualMetadataMapping
{
    public SourceObject sourceObject { get; set; }
    public TargetObject targetObject { get; set; }
    public string filterCriterion { get; set; }
    public List<ColumnMappingList> columnMappingList { get; set; }

}

class SourceObject
{
    public int mappingObjectId { get; set; }
    public string mappingObjectName { get; set; }
    public BusinessKey businessKey { get; set; }
}

class TargetObject
{
    public int mappingObjectId { get; set; }
    public string mappingObjectName { get; set; }
    public BusinessKey businessKey { get; set; }
}

class BusinessKey
{
    public string businessKeyComponentBehaviour { get; set; }
    public List<BusinessKeyComponents> businessKeyComponents { get; set; }
}

class BusinessKeyComponents
{
    public string columnName { get; set; }
}

class ColumnMappingList
{
    public SourceColumn sourceColumn { get; set; }
    public TargetColumn targetColumn { get; set; }
}

class SourceColumn
{
    public string columnName { get; set; }
    public string columnDataType { get; set; }
}

class TargetColumn
{
    public string columnName { get; set; }
    public string columnDataType { get; set; }
}
