using Person.Core.Domain.Entities.Constantes;

namespace Notification.Core.Domain.Constantes;

public static class NotificationIdentify
{
    public static Identify NOTIFICACAO_NOVO_USUARIO() => new Identify(Guid.Parse("b27ae61b-f158-48f3-a1c3-edcf00d38f8b"),
        "NOVO_USUARIO");
    
    public static Identify NOTIFICACAO_USUARIO_ATUALIZADO() => new Identify(Guid.Parse("76958cfd-19d2-4ed8-809a-f30be245e1ab"),
        "USUARIO_ATUALIZADO");
    
    public static Identify NOTIFICACAO_USUARIO_REMOVIDO() => new Identify(Guid.Parse("4e0f7d06-1e05-4895-817c-addffc81bf5e"),
        "USUARIO_REMOVIDO");
}