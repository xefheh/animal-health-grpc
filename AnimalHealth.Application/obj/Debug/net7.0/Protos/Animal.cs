// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/animal.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AnimalHealth.Application.Models {

  /// <summary>Holder for reflection information generated from Protos/animal.proto</summary>
  public static partial class AnimalReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/animal.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;
    //2
    static AnimalReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNQcm90b3MvYW5pbWFsLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0",
            "YW1wLnByb3RvIr4BCgtBbmltYWxNb2RlbBIRCglyZWdOdW1iZXIYASABKAUS",
            "DAoEbmFtZRgCIAEoCRIVCg1vd25lckZlYXR1cmVzGAMgASgJEi0KCWJpcnRo",
            "RGF0ZRgEIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASGQoRYmVo",
            "YXZpb3VyRmVhdHVyZXMYBSABKAkSCwoDc2V4GAYgASgJEgwKBHR5cGUYByAB",
            "KAkSEgoKY2hpcE51bWJlchgIIAEoBUIiqgIfQW5pbWFsSGVhbHRoLkFwcGxp",
            "Y2F0aW9uLk1vZGVsc2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AnimalHealth.Application.Models.AnimalModel), global::AnimalHealth.Application.Models.AnimalModel.Parser, new[]{ "RegNumber", "Name", "OwnerFeatures", "BirthDate", "BehaviourFeatures", "Sex", "Type", "ChipNumber" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class AnimalModel : pb::IMessage<AnimalModel>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<AnimalModel> _parser = new pb::MessageParser<AnimalModel>(() => new AnimalModel());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<AnimalModel> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AnimalHealth.Application.Models.AnimalReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AnimalModel() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AnimalModel(AnimalModel other) : this() {
      regNumber_ = other.regNumber_;
      name_ = other.name_;
      ownerFeatures_ = other.ownerFeatures_;
      birthDate_ = other.birthDate_ != null ? other.birthDate_.Clone() : null;
      behaviourFeatures_ = other.behaviourFeatures_;
      sex_ = other.sex_;
      type_ = other.type_;
      chipNumber_ = other.chipNumber_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AnimalModel Clone() {
      return new AnimalModel(this);
    }

    /// <summary>Field number for the "regNumber" field.</summary>
    public const int RegNumberFieldNumber = 1;
    private int regNumber_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int RegNumber {
      get { return regNumber_; }
      set {
        regNumber_ = value;
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "ownerFeatures" field.</summary>
    public const int OwnerFeaturesFieldNumber = 3;
    private string ownerFeatures_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string OwnerFeatures {
      get { return ownerFeatures_; }
      set {
        ownerFeatures_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "birthDate" field.</summary>
    public const int BirthDateFieldNumber = 4;
    private global::Google.Protobuf.WellKnownTypes.Timestamp birthDate_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Google.Protobuf.WellKnownTypes.Timestamp BirthDate {
      get { return birthDate_; }
      set {
        birthDate_ = value;
      }
    }

    /// <summary>Field number for the "behaviourFeatures" field.</summary>
    public const int BehaviourFeaturesFieldNumber = 5;
    private string behaviourFeatures_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string BehaviourFeatures {
      get { return behaviourFeatures_; }
      set {
        behaviourFeatures_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "sex" field.</summary>
    public const int SexFieldNumber = 6;
    private string sex_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Sex {
      get { return sex_; }
      set {
        sex_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 7;
    private string type_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Type {
      get { return type_; }
      set {
        type_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "chipNumber" field.</summary>
    public const int ChipNumberFieldNumber = 8;
    private int chipNumber_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int ChipNumber {
      get { return chipNumber_; }
      set {
        chipNumber_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as AnimalModel);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AnimalModel other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (RegNumber != other.RegNumber) return false;
      if (Name != other.Name) return false;
      if (OwnerFeatures != other.OwnerFeatures) return false;
      if (!object.Equals(BirthDate, other.BirthDate)) return false;
      if (BehaviourFeatures != other.BehaviourFeatures) return false;
      if (Sex != other.Sex) return false;
      if (Type != other.Type) return false;
      if (ChipNumber != other.ChipNumber) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (RegNumber != 0) hash ^= RegNumber.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (OwnerFeatures.Length != 0) hash ^= OwnerFeatures.GetHashCode();
      if (birthDate_ != null) hash ^= BirthDate.GetHashCode();
      if (BehaviourFeatures.Length != 0) hash ^= BehaviourFeatures.GetHashCode();
      if (Sex.Length != 0) hash ^= Sex.GetHashCode();
      if (Type.Length != 0) hash ^= Type.GetHashCode();
      if (ChipNumber != 0) hash ^= ChipNumber.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (RegNumber != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(RegNumber);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (OwnerFeatures.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(OwnerFeatures);
      }
      if (birthDate_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(BirthDate);
      }
      if (BehaviourFeatures.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(BehaviourFeatures);
      }
      if (Sex.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Sex);
      }
      if (Type.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(Type);
      }
      if (ChipNumber != 0) {
        output.WriteRawTag(64);
        output.WriteInt32(ChipNumber);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (RegNumber != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(RegNumber);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (OwnerFeatures.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(OwnerFeatures);
      }
      if (birthDate_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(BirthDate);
      }
      if (BehaviourFeatures.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(BehaviourFeatures);
      }
      if (Sex.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Sex);
      }
      if (Type.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(Type);
      }
      if (ChipNumber != 0) {
        output.WriteRawTag(64);
        output.WriteInt32(ChipNumber);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (RegNumber != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RegNumber);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (OwnerFeatures.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(OwnerFeatures);
      }
      if (birthDate_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(BirthDate);
      }
      if (BehaviourFeatures.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(BehaviourFeatures);
      }
      if (Sex.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Sex);
      }
      if (Type.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Type);
      }
      if (ChipNumber != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ChipNumber);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AnimalModel other) {
      if (other == null) {
        return;
      }
      if (other.RegNumber != 0) {
        RegNumber = other.RegNumber;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.OwnerFeatures.Length != 0) {
        OwnerFeatures = other.OwnerFeatures;
      }
      if (other.birthDate_ != null) {
        if (birthDate_ == null) {
          BirthDate = new global::Google.Protobuf.WellKnownTypes.Timestamp();
        }
        BirthDate.MergeFrom(other.BirthDate);
      }
      if (other.BehaviourFeatures.Length != 0) {
        BehaviourFeatures = other.BehaviourFeatures;
      }
      if (other.Sex.Length != 0) {
        Sex = other.Sex;
      }
      if (other.Type.Length != 0) {
        Type = other.Type;
      }
      if (other.ChipNumber != 0) {
        ChipNumber = other.ChipNumber;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            RegNumber = input.ReadInt32();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 26: {
            OwnerFeatures = input.ReadString();
            break;
          }
          case 34: {
            if (birthDate_ == null) {
              BirthDate = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(BirthDate);
            break;
          }
          case 42: {
            BehaviourFeatures = input.ReadString();
            break;
          }
          case 50: {
            Sex = input.ReadString();
            break;
          }
          case 58: {
            Type = input.ReadString();
            break;
          }
          case 64: {
            ChipNumber = input.ReadInt32();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            RegNumber = input.ReadInt32();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 26: {
            OwnerFeatures = input.ReadString();
            break;
          }
          case 34: {
            if (birthDate_ == null) {
              BirthDate = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(BirthDate);
            break;
          }
          case 42: {
            BehaviourFeatures = input.ReadString();
            break;
          }
          case 50: {
            Sex = input.ReadString();
            break;
          }
          case 58: {
            Type = input.ReadString();
            break;
          }
          case 64: {
            ChipNumber = input.ReadInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
