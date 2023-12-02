// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/pricePair.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace AnimalHealth.Application.Models {

  /// <summary>Holder for reflection information generated from Protos/pricePair.proto</summary>
  public static partial class PricePairReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/pricePair.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PricePairReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChZQcm90b3MvcHJpY2VQYWlyLnByb3RvGhVQcm90b3MvbG9jYWxpdHkucHJv",
            "dG8aFVByb3Rvcy9jb250cmFjdC5wcm90byJvCg5QcmljZVBhaXJNb2RlbBIK",
            "CgJpZBgBIAEoBRIgCghsb2NhbGl0eRgCIAEoCzIOLkxvY2FsaXR5TW9kZWwS",
            "IAoIY29udHJhY3QYAyABKAsyDi5Db250cmFjdE1vZGVsEg0KBXByaWNlGAQg",
            "ASgCQiKqAh9BbmltYWxIZWFsdGguQXBwbGljYXRpb24uTW9kZWxzYgZwcm90",
            "bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::AnimalHealth.Application.Models.LocalityReflection.Descriptor, global::AnimalHealth.Application.Models.ContractReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::AnimalHealth.Application.Models.PricePairModel), global::AnimalHealth.Application.Models.PricePairModel.Parser, new[]{ "Id", "Locality", "Contract", "Price" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class PricePairModel : pb::IMessage<PricePairModel>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<PricePairModel> _parser = new pb::MessageParser<PricePairModel>(() => new PricePairModel());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<PricePairModel> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::AnimalHealth.Application.Models.PricePairReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PricePairModel() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PricePairModel(PricePairModel other) : this() {
      id_ = other.id_;
      locality_ = other.locality_ != null ? other.locality_.Clone() : null;
      contract_ = other.contract_ != null ? other.contract_.Clone() : null;
      price_ = other.price_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PricePairModel Clone() {
      return new PricePairModel(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "locality" field.</summary>
    public const int LocalityFieldNumber = 2;
    private global::AnimalHealth.Application.Models.LocalityModel locality_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AnimalHealth.Application.Models.LocalityModel Locality {
      get { return locality_; }
      set {
        locality_ = value;
      }
    }

    /// <summary>Field number for the "contract" field.</summary>
    public const int ContractFieldNumber = 3;
    private global::AnimalHealth.Application.Models.ContractModel contract_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::AnimalHealth.Application.Models.ContractModel Contract {
      get { return contract_; }
      set {
        contract_ = value;
      }
    }

    /// <summary>Field number for the "price" field.</summary>
    public const int PriceFieldNumber = 4;
    private float price_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float Price {
      get { return price_; }
      set {
        price_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as PricePairModel);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(PricePairModel other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (!object.Equals(Locality, other.Locality)) return false;
      if (!object.Equals(Contract, other.Contract)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Price, other.Price)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (locality_ != null) hash ^= Locality.GetHashCode();
      if (contract_ != null) hash ^= Contract.GetHashCode();
      if (Price != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Price);
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
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (locality_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Locality);
      }
      if (contract_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Contract);
      }
      if (Price != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Price);
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
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (locality_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Locality);
      }
      if (contract_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Contract);
      }
      if (Price != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Price);
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
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (locality_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Locality);
      }
      if (contract_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Contract);
      }
      if (Price != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(PricePairModel other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.locality_ != null) {
        if (locality_ == null) {
          Locality = new global::AnimalHealth.Application.Models.LocalityModel();
        }
        Locality.MergeFrom(other.Locality);
      }
      if (other.contract_ != null) {
        if (contract_ == null) {
          Contract = new global::AnimalHealth.Application.Models.ContractModel();
        }
        Contract.MergeFrom(other.Contract);
      }
      if (other.Price != 0F) {
        Price = other.Price;
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
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            if (locality_ == null) {
              Locality = new global::AnimalHealth.Application.Models.LocalityModel();
            }
            input.ReadMessage(Locality);
            break;
          }
          case 26: {
            if (contract_ == null) {
              Contract = new global::AnimalHealth.Application.Models.ContractModel();
            }
            input.ReadMessage(Contract);
            break;
          }
          case 37: {
            Price = input.ReadFloat();
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
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            if (locality_ == null) {
              Locality = new global::AnimalHealth.Application.Models.LocalityModel();
            }
            input.ReadMessage(Locality);
            break;
          }
          case 26: {
            if (contract_ == null) {
              Contract = new global::AnimalHealth.Application.Models.ContractModel();
            }
            input.ReadMessage(Contract);
            break;
          }
          case 37: {
            Price = input.ReadFloat();
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
