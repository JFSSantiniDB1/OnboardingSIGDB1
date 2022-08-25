using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Dto;
using Xunit;

namespace OnboardingSIGDB1.Test.Util;

public static class AssertExtension
{
    public static void ComMensagemEsperada(this Notification? notification, string mensagem)
    {
        if(notification?.Message == mensagem)
            Assert.True(true);
        else
            Assert.False(true, $"Esperava a mensagem '{mensagem}' " +
                               @$"{(notification == null ? "mas não obteve mensagem alguma." :
                                   $"mas a mensagem é '{notification?.Message}'.")}");
    }
    
    public static void ComMensagemEsperada(this ValidationFailure? validation, string mensagem)
    {
        if(validation?.ErrorMessage == mensagem)
            Assert.True(true);
        else
            Assert.False(true, $"Esperava a mensagem '{mensagem}' " +
                               @$"{(validation == null ? "mas não obteve mensagem alguma." :
                                   $"mas a mensagem é '{validation?.ErrorMessage}'.")}");
    }
}