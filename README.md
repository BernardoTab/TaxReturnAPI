# Tax Return API

## Description
This project is part of GlobalBlue's interview process. The idea is to allow the user to input an amount value with a VAT rate and be given the remaining computed values.

You can input one of the following values:
- **NetValue**: The price before VAT is added.
- **GrossValue**: The price after VAT has been applied.
- **VATValue**: The amount of VAT added to the Net Value.

You must also provide the AustrianVATRate, which can be 10%, 13%, or 20%.

## Usage

You'll want to send a POST request to the endpoint '/api/Taxes' and in the Body you fill in the values you need

Example of POST request body:
`{
    "NetValue":50,
    "AustrianVATRate":"10"
}`

The amount values should be of type 'decimal' and you can only input one of the amount values. 
Additionally, the AustrianVATRate should be a string of values '10', '13' or '20'. You can also use 'VAT10Percent', 'VAT13Percent' or 'VAT20Percent'.

Wrong inputs are validated according to the assignment

## Assumptions and Comments

I assumed that this assignment would evaluate scalability and reusability so I organized things in a way that would help improve the assignment, if necessary.

TaxReturnInfo isn't, in my opinion, really an entity as it has no ID and isn't part of the domain model (since there isn't any) but, for organization's sake, I considered it to be one in this project.

The assignment doesn't request this but I limited the input validations so that they would not accept negative numbers as to me it wouldn't make much sense to calculate negative values, if this is wrong I apologize. I also restricted the options for the VAT Rate so only the austrian values can be picked. 

It is possible that the calculations may be incorrect as I sometimes confuse Gross and Net, however, the values seem to match the calculator that was provided. If there is any error in calculation please let me know as this is easily adjustable. I also assumed a midpoint rounding with 2 decimal units would be ok.

I added Swagger for testing purposes so feel free to use it too if you'd like

## Tests

I've prepared both unit tests (Tax.Services.Implementations.UnitTests) and acceptance tests (Tax.AcceptanceTests) for this project, if you'd like to run them you can move to their respective directory and execute the command `dotnet test` or use an IDE

## How to run the API

### Clone the repository and run directly locally

You can clone this repository and install the correct .NET SDK version (.NET 8.0), then follow the steps:

1. Open the .sln file in Visual Studio or open everything in Visual Studio Code
2. Run `dotnet restore` to ensure the NuGet dependencies are set
3. `dotnet build`
4. `dotnet run`

Then you can simply send requests via HTTP (port 5164) or HTTPS (port 7032) at the appropriate port

### Run docker-compose (only HTTP)

As an alternative, I've added a Dockerfile and a docker-compose.yml for an easy setup.

1. Install Docker Desktop (if on Windows)
2. Run the command `docker-compose up -d` to get the container running in the background
3. I've enabled only HTTP on docker and the port is 5164 so you can now send requests to localhost:5164 via HTTP