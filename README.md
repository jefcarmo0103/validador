# Validador de Senhas

Projeto simples para realizar a validação de senhas com base em um conjunto de regras de negócio.

## Como executar

**Via Visual Studio:** abra a solution `Validador.slnx` e execute o projeto `Validador.API`.

**Via terminal:**

```bash
dotnet build
dotnet run --project Validador.API
```

A documentação interativa estará disponível em `/swagger` ao rodar em modo desenvolvimento.

## Endpoint

```
POST /api/password/validate
Content-Type: application/json

{ "password": "SuaSenha@1" }
```

**Senha válida — 200 OK:**
```json
{ "isValid": true }
```

**Senha inválida — 400 Bad Request:**
```json
{
  "isValid": false,
  "errors": [
    "Deve conter ao menos 1 dígito.",
    "Não deve possuir caracteres repetidos."
  ]
}
```

## Regras de validação

- Nove ou mais caracteres
- Ao menos 1 dígito
- Ao menos 1 letra minúscula
- Ao menos 1 letra maiúscula
- Ao menos 1 caractere especial (`!@#$%^&*()-+`)
- Sem caracteres repetidos

## Arquitetura

Foi adotada a **arquitetura hexagonal** por ser a forma que melhor representa o negócio através do código, mantendo as regras de domínio isoladas de detalhes de infraestrutura e framework.

```
Validador.Domain       → regras de negócio, entidades e interfaces (ports)
Validador.Application  → casos de uso
Validador.API          → adaptadores HTTP (controllers, middleware)
Validador.UnitTest     → testes unitários
```

### Tratamento de erros

Um middleware centraliza a captura de exceções — tanto as específicas de validação de domínio (`PasswordValidationException`) quanto as genéricas não tratadas. Isso mantém os controllers limpos e garante respostas consistentes para o cliente.

### Estratégia de retorno de erros

Todas as mensagens de erro são empilhadas e devolvidas de uma só vez, permitindo que o usuário identifique e corrija todos os pontos inválidos antes de tentar novamente.