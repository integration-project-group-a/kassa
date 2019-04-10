using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlrpcAPI.Dto
{
    
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Message
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

    public string MessageType { get; set; }
    public string description { get; set; }
    public string sender { get; set; }
}

[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class MessageDatastructure
{
    public object UUID { get; set; }
    public MessageDatastructureName name { get; set; }
    public object email { get; set; }
    public object timestamp { get; set; }
    public object version { get; set; }
    public object isActive { get; set; }
    public object banned { get; set; }
    public object birthdate { get; set; }
    [System.Xml.Serialization.XmlElementAttribute("btw-nummer")]
    public object btwnummer { get; set; }
    [System.Xml.Serialization.XmlElementAttribute("gsm-nummer")]
    public object gsmnummer { get; set; }
    public object GDPR { get; set; }
    public object extraField { get; set; }
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