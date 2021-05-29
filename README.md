# CSharpDotNetAPI

## O que é: 

API desenvolvida com o intúito de possibilitar a criação de um sistema com um banco de dados que ligue o e-mail e o nome
de uma conta cadastrada, ao seu cartão de crédito físico, e aos seus cartões virtuais.

## Models

  Os **Models** indicam a forma dos dados e os requisitos para estes serem aceitos, assim como cria as mensagens de erro
  caso o requisitos não sejam alcançados. Essa fase é essencial para manter a coerência dos dados. Em ambos os Models,
  foram adicionados pré-requisitos que cada variável deveria cumprir para ser aceita e inserida no banco de dados.

### CardInfo

  O **CardInfo** trata dos dados do Cartão do Cliente que indica obrigatoriedade de preenchimento e limita o tamanho do 
  número do cartão (*CardNumber*) e associa a uma conta pelo seu identificador(*AccountId*).

    ```csharp
    //CardNumber
    [Required(ErrorMessage = "Campo obrigatório.")]
    [MaxLength(16, ErrorMessage = "Este campo deve conter 16 dígitos.")]
    [MinLength(16, ErrorMessage = "Este campo deve conter 16 dígitos.")]   
     public string CardNumber { get; set; }

    //AccountId
    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida.")]
    public int AccountId {get; set; }

    ```

### AccountInfo

  O **AccountInfo** é responsável por tratar os dados principais, o e-mail do usuário e seu nome, que serão as inserções
  iniciais, por onde o sistema ira associar os números dos cartões.

    ```csharp
    
    //Email
    [Required(ErrorMessage = "Campo obrigatório.")]
    [MaxLength(30, ErrorMessage = "Este campo deve conter entre 11 e 30 caracteres.")]
    [MinLength(11, ErrorMessage = "Este campo deve conter entre 11 e 30 caracteres.")]
    public string Email { get; set; }

    //AccountName
    [Required(ErrorMessage = "Campo obrigatório.")]
    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
    public string AccountName { get; set; }

    ```

## Controllers

Os **Controllers** são responsáveis pela parte do retorno dos dados e como eles serão apresentados, todo o processo de
consulta e alteração dos dados acontece nessa parte do projeto, com as funções dos métodos HTTP.

Com um Método POST em cada **Controller** para inserção dos dados e Métodos GETs variados para diferentes tipos de 
consultas diferentes. 

### CardControllers

```csharp
    
  [ApiController]
  [Route("v1/cards")]
  public class CardController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<CardInfo>>> Get([FromServices] DataContext context)
    {
      var cardNumber = await context.CardInfos.Include(x => x.AccountInfo).ToListAsync();
      return cardNumber;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<CardInfo>> GetById([FromServices] DataContext context, int id)
    {
      var cardType = await context.CardInfos.Include(x => x.AccountInfo)
        .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
      return cardType;
    }

    [HttpGet]
    [Route("/accounts/{id:int}")]
    public async Task<ActionResult<List<CardInfo>>> GetByAccount([FromServices] DataContext context, int id)
    {
      var cardNumber = await context.CardInfos.Include(x => x.CardNumber).AsNoTracking()
        .Where(x => x.AccountId == id & x.CardType == "Virtual").ToListAsync();
      return cardNumber;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<CardInfo>> Post([FromServices] DataContext context, [FromBody] CardInfo model)
    {
      if (ModelState.IsValid)
      { 
        context.CardInfos.Add(model);
        await context.SaveChangesAsync();
        return model;
      }
      else 
      {
        return BadRequest(ModelState);
      }

    ```

    ### AccountControlers:
    
```csharp
    
  [ApiController]
  [Route("v1/accounts")]
  public class AccountController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<AccountInfo>>> Get([FromServices] DataContext context)
    {
      var infos = await context.AccountInfos.ToListAsync();
      return infos;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<AccountInfo>> Post([FromServices] DataContext context, [FromBody] AccountInfo model)
    {
      if (ModelState.IsValid)
      {
        context.AccountInfos.Add(model);
        await context.SaveChangesAsync();
        return model;
      }
      else
      {
        return BadRequest(ModelState);
      }

    ```
