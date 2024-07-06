
# Blog com notificações via WebSockets

## Descrição
Criar um sistema básico de blog onde os usuários podem visualizar, criar, editar e excluir postagens. O projeto deve utilizar os princípios de orientação a objetos, seguir os princípios SOLID, integrar o Entity Framework para manipulação de dados e incluir uma comunicação simples usando WebSockets para notificar os usuários sobre novas postagens em tempo real.

## Requisitos Funcionais:
**Autenticação:** Usuários devem ser capazes de se registrar, fazer login.

**Gerenciamento de Postagens:** Os usuários autenticados podem criar postagens, editar suas próprias postagens e excluir postagens existentes.

**Visualização de Postagens:** Qualquer visitante do site pode visualizar as postagens existentes.

**Notificações em Tempo Real:** Implemente um sistema de notificação em tempo real usando WebSockets para informar os usuários sobre novas postagens assim que são publicadas.

## Requisitos Técnicos:
Utilize a arquitetura monolítica organizando as responsabilidades, como autenticação, gerenciamento de postagens e notificações em tempo real.
Aplique os princípios SOLID, especialmente o princípio da Responsabilidade Única (SRP) e o princípio da Inversão de Dependência (DIP).
Utilize o Entity Framework para interagir com o banco de dados para armazenar informações sobre usuários e postagens.
Implemente WebSockets para notificações em tempo real. Pode ser uma notificação simples para interface do usuário sempre que uma nova postagem é feita

# Instalando e preparando o ambiente

### Pré requisitos

 - .NET 8 (SDK)
 - IDE de sua preferência, mas nos exemplos a seguir irei utilizar o Visual Studio

### Clonando o repositório
1.  Crie um diretório para o projeto no seu computador.
2.  Abra o terminal no repositório criado.
3.  Execute o comando abaixo:
`git clone https://github.com/DiegoNetoMartins/blog-websocket `
Ou se preferir, realize o download do código fonte em formato .zip e extraia para uma pasta de sua preferência:

![image](https://github.com/DiegoNetoMartins/blog-websocket/assets/82280204/94c27024-fad9-405d-a876-d37aa545a144)


### Configurando e executando o projeto
- Abra o projeto com o Visual Studio e configure para inicializar automaticamente o projeto "Blog.Api" e "Blog.Client" conforme imagem abaixo:

![image](https://github.com/DiegoNetoMartins/blog-websocket/assets/82280204/fda1c61e-4ee4-4c2b-82f2-c4b94c7ff30c)


Com isso, será aberto a documentação da API e o aplicativo console para simular 3 clientes conectados no blog que deverão receber as notificações em tempo real ao criar um novo post.

![image](https://github.com/DiegoNetoMartins/blog-websocket/assets/82280204/6c71e0f2-1370-41d0-a054-5c02804cd4ee)


### Criando um usuário
- Antes de tudo, é necessário criar um usuário, para poder autenticar no Swagger e criar o post posteriormente e validar se o aplicativo console receberá as notificações ao criar um novo post.
![image](https://github.com/DiegoNetoMartins/blog-websocket/assets/82280204/ea0ca837-d586-4b4c-acc0-c0340a426177)

### Autenticando com o usuário criado
- Depois de criar o usuário é necessário se autenticar para que a aplicação gere o token para poder criar um post.
![image](https://github.com/DiegoNetoMartins/blog-websocket/assets/82280204/b6f0efaa-624b-45b4-b09a-cb69948d0382)
- Agora copie o token de autenticação e insira na caia de texto de autorização do Swagger:
![image](https://github.com/DiegoNetoMartins/blog-websocket/assets/82280204/e14d5a19-8f0e-4e12-8e8b-3ddd5ee45f05)

Pronto, agora podemos criar o post.
### Criando um novo post
- Depois de estar autenticado, podemos criar um novo post de exemplo e validar se os usuários conectados no blog receberão a notificação do novo post criado em tempo real:
![image](https://github.com/DiegoNetoMartins/blog-websocket/assets/82280204/5b841225-8791-4bf7-a114-f0b4d060e62e)
- Pronto, depois de criar o post podemos verificar que os 3 clientes simulados pelo console application receberam a notificação em tempo real:
![image](https://github.com/DiegoNetoMartins/blog-websocket/assets/82280204/dd6f37cd-a829-4174-bbb7-9e745c2c7e43)


