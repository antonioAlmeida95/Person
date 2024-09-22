namespace Person.Core.Domain.Entities.Constantes;

public class IdentifyRegister
{
    public static readonly IdentifyRegister Instance = new IdentifyRegister();
}
    
public class Identify
{
    public Guid Identificador { get; private set; }
    public string Nome { get; private set; }

    public Identify(Guid id, string nome)
    {
        Identificador = id;
        Nome = nome;
    }
        
    public static implicit operator Guid(Identify obj) => obj.Identificador;
    public static implicit operator string(Identify obj) => obj.Nome;
}