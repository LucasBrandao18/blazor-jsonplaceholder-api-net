# BlazorUsersJsonPlaceHolderApi

Aplicação Blazor WebAssembly em C# e .NET 10, desenvolvida com base no arquivo **lista.pdf**.

## O que foi solicitado

A atividade pede criar uma aplicação Blazor que consulte a lista de usuários em [JSONPlaceholder — /users](https://jsonplaceholder.typicode.com/users) e imprima o **nome**, a **cidade** e a **empresa da primeira pessoa da lista** no console.

O projeto apresenta os dados na página e também executa `Console.WriteLine` para as três informações. Como o C# roda no navegador com Blazor WebAssembly, essa saída aparece no **console do navegador**. O terminal de `dotnet run` mostra as mensagens do servidor de desenvolvimento.

Os itens de criação/publicação do repositório no GitHub e entrega no Google Classroom serão realizados manualmente pelo aluno. O nome de repositório sugerido no PDF é `blazor-jsonplaceholder-api-net`.

## Requisitos

- SDK do .NET 10 instalado (`dotnet --version`).
- Navegador atualizado e acesso à internet para consultar a API.
- Internet na primeira restauração dos pacotes NuGet.

Não é necessário cadastrar chave de API nem configurar banco de dados.

## Como executar

No PowerShell:

```powershell
cd "C:\Users\lucas\OneDrive\Documentos\vs code uni\homework-APIJsonPlaceHolder"
dotnet restore
dotnet run
```

Abra [http://localhost:5223](http://localhost:5223). A consulta começa automaticamente ao abrir a página. O botão **Consultar novamente** faz outra requisição e atualiza os dados. Para encerrar, pressione `Ctrl+C` no terminal.

No VS Code, abra a pasta raiz e use **Terminal > Executar Tarefa > executar BlazorUsersJsonPlaceHolderApi**. Há também uma tarefa de compilação em `.vscode/tasks.json`.

Para ver a saída pedida no console, abra as ferramentas do navegador com `F12`, selecione **Console** e clique em **Consultar novamente**. Na API consultada durante a validação, a saída foi:

```text
Nome: Leanne Graham
Cidade: Gwenborough
Empresa: Romaguera-Crona
```

Esses valores são exemplos da resposta real, não estão fixados no código. Se a ordem ou os dados da API mudarem, a aplicação mostrará a primeira pessoa da nova resposta.

## Como o código funciona

1. `Program.cs` registra o `HttpClient` na injeção de dependências, com limite de 15 segundos para a consulta.
2. `Pages/Home.razor` chama `CarregarUsuario` no ciclo de inicialização assíncrono `OnInitializedAsync`.
3. `GetFromJsonAsync<List<Usuario?>>` faz uma requisição HTTP GET ao endpoint `/users` e converte o JSON em uma lista de objetos C#.
4. Após verificar a resposta, o componente acessa `usuarios[0]`. A ordem da API é preservada: não há ordenação, sorteio, seleção por ID ou consulta a `/users/1`.
5. A página acessa `Nome`, `Endereco.Cidade` e `Empresa.Nome`, exibe os valores e escreve as três linhas no console.

Os atributos `JsonPropertyName` relacionam os nomes do JSON às propriedades C#: `name` corresponde a `Nome`, `address` a `Endereco`, `city` a `Cidade` e `company` a `Empresa`. Os modelos incluem apenas os campos necessários ao exercício; os demais campos da resposta são ignorados pelo desserializador.

Durante a consulta, o botão fica desabilitado e a página informa o carregamento. O componente trata falha HTTP/rede, tempo limite, JSON inválido, resposta nula, lista vazia e primeira pessoa inválida. Se cidade ou empresa estiverem ausentes, mostra **Não informado pela API**. A requisição pendente é cancelada quando o componente é encerrado. O botão permite tentar novamente após uma falha.

## Arquivos principais

| Arquivo | Responsabilidade |
| --- | --- |
| `BlazorUsersJsonPlaceHolderApi.csproj` | Configura .NET 10 e os pacotes do Blazor WebAssembly. |
| `Program.cs` | Inicializa o aplicativo e registra o cliente HTTP. |
| `Pages/Home.razor` | Consulta a API, valida a resposta, exibe os dados e escreve no console. |
| `Pages/Home.razor.css` | Limita a largura do cartão e permite quebrar textos longos. |
| `Models/Usuario.cs` | Representa o usuário e seus objetos de endereço e empresa. |
| `Models/Endereco.cs` | Representa a cidade do endereço. |
| `Models/Empresa.cs` | Representa o nome da empresa. |
| `Layout/` | Define a estrutura visual e o menu. |
| `App.razor` e `_Imports.razor` | Configuram rotas e importações dos componentes. |
| `wwwroot/index.html` | Página que carrega o aplicativo no navegador. |
| `Properties/launchSettings.json` | Configura o endereço local de execução. |
| `tests/BlazorUsersJsonPlaceHolderApi.Tests/` | Testes de componente com bUnit e xUnit. |

## Compilação e testes

Na raiz do projeto:

```powershell
dotnet build
dotnet test "tests\BlazorUsersJsonPlaceHolderApi.Tests\BlazorUsersJsonPlaceHolderApi.Tests.csproj"
```

Foram implementados e aprovados **13 testes**, cobrindo a consulta automática, o endereço correto da API, a preservação da primeira posição da lista, respostas vazias ou inválidas, campos opcionais ausentes, falhas HTTP/rede, tempo limite, nova tentativa e estado de carregamento.

Os testes usam respostas HTTP simuladas para não depender da disponibilidade da API. A consulta real também foi validada no navegador, incluindo a saída no console e o botão de nova consulta. A pasta `tests` é excluída dos arquivos do aplicativo principal pelo `.csproj`, portanto os testes não são enviados como parte do aplicativo ao navegador.

O `.gitignore` ignora arquivos de compilação como `bin/` e `obj/`, facilitando a publicação manual do código-fonte.
