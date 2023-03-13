# Tryitter

Projeto de conclusão de Aceleração Voalle em C#

Grupo: Felipe Rangel Bezerra

### Como rodar

Para criar o banco de dados rode o comando:

```docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password12' -p 1433:1433 -d mcr.microsoft.com/mssql/server```

Após criar o banco de dados, espere uns minutos para o banco de dados iniciar, corrija o nome do arquivo .env.template para .env e rode o comando: ```dotnet run``` dentro da pasta Tryitter
