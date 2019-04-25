using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlrpcAPI.Dto
{
    
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "message")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Visitor
{
    public MessageHeader header { get; set; }
    public MessageDatastructure datastructure { get; set; }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class MessageHeader
{

    public string messageType { get; set; }
    public string description { get; set; }
    public string sender { get; set; }
}

[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class MessageDatastructure
{
    public string UUID { get; set; }
    public MessageDatastructureName name { get; set; }
    public string email { get; set; }
    public string timestamp { get; set; }
    public string version { get; set; }
    public bool isActive { get; set; }
    public bool banned { get; set; }
    [System.Xml.Serialization.XmlElementAttribute("geboortedatum")]
    public string dateOfBirth { get; set; }
    [System.Xml.Serialization.XmlElementAttribute("btw-nummer")]
    public string btwNumber { get; set; }
    [System.Xml.Serialization.XmlElementAttribute("gsm-nummer")]
    public string gsmNumber { get; set; }
    public bool GDPR { get; set; }
    public string extraField { get; set; }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class MessageDatastructureName
{
    public string firstname { get; set; }
    public string lastname { get; set; }
}
}