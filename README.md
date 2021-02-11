# TesteQA-RicardoEletro-IsItBrastemp
Conjunto de testes automatizados desenvolvidos em Selenium para efetuar a compra de uma geladeira Brastemp

Os testes foram desenvolvidos com o auxílio do Selenium IDE, e então exportados para C# e refinados no Visual Studio para execução no Selenium WebDriver. Alternativamente, os testes podem ser executados usando NUnit. A documentação interna está em inglês.

Este repositário contém:
1) Sketch: Esboço do projeto, desenvolvido no Selenium IDE.
- Para executá-lo, baixe e instale o Plugin Selenium IDE em seu Browser (Versão utilizada: 3.17.0)
- Abra-o em seu navegador e opte por abrir um projeto existente (Atalho: CTRL+O) e forneça o arquivo RicardoEletro.side na pasta Sketch deste repositório
- Em seguida, procure o botão Test Execution Speed (Se assemelha a um relógio) e ajuste para a velocidade mais baixa, para evitar erros
- Execute o caso de teste desejado (Atalho: CTRL+R) ou todos eles em sequência (Atalho: CTRL+Shift+R)

2) WebDriver: Projeto exportado para C# / NUnit e refinado com o auxílio do Visual Studio
- Para executá-lo, é recomendado ter o Visual Studio configurado para a execução de testes Selenium pelo navegador de sua escolha (utilizei o Firefox)
- Plugins utilizados: Selenium.WebDriver (v3.141.0), Selenium.WebDriver.GeckoDriver (v0.29.0), Selenium.Support (v3.141.0), NUnit (v3.13.1), NUnit3TestAdapter (v3.17)
- Carregue o projeto no VS ou no ambiente de sua escolha
- Abra a janela "Test Explorer" e, com o botão direito, dê inicio aos testes de sua escolha (Atalho: CTRL+R, depois T)
- Reconfigure as variáveis que desejar no método SetUp de cada teste

3) NUnit: Projeto final exportado para NUnit, para fácil execução
- É preciso baixar e instalar o NUnit Console em seu computador (Versão Utilizada: 3.12)
- É necessário, ou ao menos mais cômodo, adicionar o NUnit às Variáveis de Ambiente caso esteja utilizando Windows
- Baixe o arquivo RodarTestesRicardoEletro.dll e clique nele com o botão direito enquanto segura a tecla shift. Então clique em "Copiar como caminho"
- Execute o comando nunit3-console PATH, sendo PATH o caminho que você copiou no passo anterior



A bateria de testes (ST01BuyBrastemp) consiste em três testes, que consideram fluxos alternativos:

CT01Search:
- Cliente procura um produto (o texto da busca pode ser ajustado na variável searchText do SetUp do teste)
- Caso Base: Produtos foram encontrados com sucesso
- Fluxo Alternativo: Nenhum produto foi encontrado

Também:
- Verifica a existência da barra e do botão de busca
- Verifica a existência de textos e botões esperados na página de resultados

CT02SelectProduct:
- Cliente procura um produto
- Cliente ordena os resultados pela ordem de melhores avaliações
- Cliente escolhe um produto dentro de uma faixa de preço (ajustável na variável priceMin e priceMax)
- Cliente verifica se o produto é uma Brastemp
- Fluxo Alternativo: O produto não é uma Brastemp. O Cliente continua a escolher.
- Cliente compra o produto
- Caso Base: O produto se encontra no carrinho de compras
- Fluxo Alternativo: O carrinho está vazio

CT03CheckCartTest:
- Cliente procura um produto
- Cliente adiciona o produto ao carrinho de compras
- Cliente acessa o carrinho
- Cliente confere sua compra
- Fluxo Alternativo: O carrinho está vazio
- Fluxo Alternativo: O carrinho contém mais de um item
- Fluxo Alternativo: O preço total da compra não confere com o preço unitário do produto
- Fluxo Alternativo: O produto não é uma Brastemp
- Caso Base: A compre está de acordo com a espectativa do cliente

Também:
- Verifica o funcionamento dos botões para entrar e sair do carrinho de compras
- Verifica a existência de textos, botões e ícones esperados na página do carrinho
- É uma versão mais completa do teste executado ao final do CT02, cujo objetivo era apenas verificar se o item foi adicionado ao carrinho
