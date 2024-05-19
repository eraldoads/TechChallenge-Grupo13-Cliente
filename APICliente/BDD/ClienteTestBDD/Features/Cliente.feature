Funcionalidade: Gerenciar Clientes

Como usuário do sistema 
Eu quero buscar um Cliente por seu ID

@tag1
Cenario: [Buscar Cliente por ID]
	Dado que um Cliente já foi cadastrado
	Quando requisitar a busca do Cliente por ID
	Então o Cliente é exibido com sucesso