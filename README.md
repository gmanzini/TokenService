# TokenService
<h1 align="center">Token Generation</h1>

## The project
<p align="center">API Responsible for managing token generation for cashless registration</p>




<p align="center">
 <a href="#Features">Features</a> •
  <a href="#Prerequirement">Prerequirement</a> • 
 <a href="#Technologies">Technologies</a> • 
 <a href="#autor">Autor</a>
</p>


### Features

- [x] Save Card and Generate the Token
- [x] Validate Token


### Prerequirement

Before you start, you`re going to need the following tools: 
[Git](https://git-scm.com), [.NET Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1), [Docker Desktop](https://www.docker.com/products/docker-desktop/).
Besides that, a good IDE like [Visual Studio](https://visualstudio.microsoft.com/)



### 🛠 Technologies

The following tools were used in the project:

- [Visual Studio](https://visualstudio.microsoft.com/)
- [.NET Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1)
- [Docker](https://www.docker.com/products/docker-desktop/)
- [Swagger](https://swagger.io/)
- [Entity Framework](https://docs.microsoft.com/en-us/ef/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [XUnit](https://xunit.net/)

### 🎲 Running the API (server)

```bash
# Clone this repo
$ git clone https://github.com/gmanzini/TokenService.git

# 
$ Setup Docker profile as startup profile
Right Click on TokenGeneratorService Project > Properties > Debug
```

![Screenshot 2022-07-08 114716](https://user-images.githubusercontent.com/54852015/178019422-6ad6142c-6552-4203-9e09-ade2434c58a8.jpg)
```
$ Change start from IIS Express to Docker (This will verify that docker is installed correctly and pull the images )
```
![image](https://user-images.githubusercontent.com/54852015/178029492-eabd77c0-cc32-403e-ad30-f68a04a78bfe.png)

```
$ Connecting to local or remote SQL Server from Docker Containers

1. You must configure SQL Server with Mixed Mode Authentication. For remote connection you need to supply user name and password.
2. Open SQL Server Configuration Manager as Administrator.
```
![image](https://user-images.githubusercontent.com/54852015/178027604-6d462ce7-528e-4fb6-9ce8-30f350475643.png)
```
3.Now select Protocols for SQL and then select TCP/IP now open the Properties by right click.
4.In the TCP/IP properties window enable TCP/IP for SQL Server (Enabled=Yes, Listen All=Yes).
```
![image](https://user-images.githubusercontent.com/54852015/178027683-8e62d221-faf1-467a-8eab-d8d9772a3a51.png)
```
5.Now select IP Addresses tab in properties window. And under IPAll give port number as 49172.
```
![image](https://user-images.githubusercontent.com/54852015/178027748-19903a33-8688-4587-811b-4f533c8c3b53.png)
```
6. Now restart SQL Server service.
```
![image](https://user-images.githubusercontent.com/54852015/178027828-0e517991-e756-4b09-a95f-7f2cea602ea9.png)
```
7.Make sure SQL Server Browser is running, if not start it.
```
![image](https://user-images.githubusercontent.com/54852015/178027877-b4995e2d-5b7d-4725-ae12-c5035d198b16.png)
```
8. Now setup your firewall to accept inbound connection to 49172.
```
![image](https://user-images.githubusercontent.com/54852015/178027956-b8cb623f-7692-4a86-8eb0-3c8b29a69c6a.png)
```
9.To connect to sql server from docker, you can’t use the host computer name. You need to find the right ip address of you host.

10 . Open command prompt and issue “ipconfig” command, Then you can see a nat ip address of Docker NAT ip address copy it.
```
![image](https://user-images.githubusercontent.com/54852015/178028061-f04c99c7-57ef-448e-b4e5-491f8a612c34.png)

```
11. Overwrite the ip with the NAT IP address from the previous step
```
![image](https://user-images.githubusercontent.com/54852015/178028369-c413080f-2980-4876-83fd-047e18404580.png)



```
$To create the database locally, all you have to do is ensure your connection is working and run the migration command from the package manager console:
update-database
```
![image](https://user-images.githubusercontent.com/54852015/178031281-2e7bb500-809f-4333-99dc-7e8e0ac0240b.png)
### Autor
---


 <br />
 <sub><b>Gabriel Manzini</b></sub></a> <a href="https://github.com/gmanzini" title="Manzini">🚀</a>


Made by ❤️  Gabriel Manzini 👋🏽 Feel free to contact me!


[![Gmail Badge](https://img.shields.io/badge/-manzini.gabriel@hotmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:manzini.gabriel@hotmail.com)](mailto:manzini.gabriel@hotmail.com)
