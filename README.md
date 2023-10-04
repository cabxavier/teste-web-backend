# teste-web-backend

######### BACKEND #########
1 º Passo: Clonar o repósitorio na máquina, e acessar a pasta aonde foi feito o clone do projeto, exemplo: -> C:\teste-web-backend
2 º Passo: A través do Visual Studio, acessar a raiz do projeto na pasta -> /my-api-teste
3 º Passo: O banco utilizado no projeto foi o MS SqlServer, dentro da pasta -> my-api-teste\DataBase\script, executar nessa ordem os scripts -> script_01.sql, em seguida script_02.sql, com isso foi criado a base de dados com o nome de -> TesteWebCesar, juntamente com as tabelas e inserts necessários
4 º Passo: Através do Visual Studio, alterar a string de conexão com o banco no arquivo -> Web.config, tag -> connectionStrings, é necessário alterar a propriedade -> Data Source, nessa propriedade colocar o nome do servidor aonde se encontra o banco de dados instalado, esse parâmetro se encontra no MS SqlServer Management Studio, e com a tecla de Atalho F4, -> propriedade Server name (Nome do servidor), depois alterar a propriedade -> Password, colocar a senha do banco de dados
5 º Passo: Através do Visual Studio, executar a solution, o projeto estará rodando no navegador no o endereço: -> http://localhost:54303/

Obs: É necessário ter o Visual Studio e o banco de dados MS SqlServer, juntamente com MS SqlServer Management Studio, instalado na máquina, segue os links abaixo caso seja necessário para download:
-> Visual Studio: https://visualstudio.microsoft.com/pt-br/downloads/
-> MS SqlServer Management Studio: https://learn.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16