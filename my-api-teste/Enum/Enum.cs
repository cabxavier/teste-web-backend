namespace Enum
{
    public enum PostAction { Insert, Update, Delete };

    public enum DataDefinition
    {
        Indefined,
        TextWithVariantLength,
        TextWithFixedLength,
        Boolean,
        Decimal,
        Integer,
        DateAndTime,
        Date,
        Time,
        Cep,
        Cnpj,
        Cpf,
        Placa,
        Email
    };
}
