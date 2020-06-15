# Wallet App

The purpose of this project is to practice DDD + CQRS making a wallet app. Go to https://github.com/cealer/WalletProject/wiki/WalletProject-high-level-system-design to more information.

### Getting Started 🚀

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on production enviroment.

### Prerequisites 📋
```
.Net Core 3.1
Coverlet (Test Coverage)
```

# Wallet
### Create
_Create a wallet per user._

_Post Method: https://localhost:44301/api/wallets_

Body Json:
```
 {
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
 }
```
### Info
_Get Wallet info. An user can see his current balance._

_GET METHOD: https://localhost:44301/api/wallets/{userId} example:_
```
  https://localhost:44301/api/wallets/3fa85f64-5717-4562-b3fc-2c963f66afa6
```
### Deposit
_Deposit an amount to a wallet._

_Post Method: https://localhost:44301/api/wallets/deposit_

Body Json:
```
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "amount": 100
}
```
# Payments
### Create
_Create a payment that reduces the user's wallet balance._ 
_There are 3 payments methods: Cash (1), Bitcoin (2), GooglePay (3)._

_POST METHOD: http://localhost:61945/Payments_

_Body Json_
```
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "amount": 10,
  "paymentMethodId": 1
}
```
### List
_List all user's payments._ 

_GET METHOD: https://localhost:44301/api/payments/{userId} example:_
```
  https://localhost:44301/api/payments/3fa85f64-5717-4562-b3fc-2c963f66afa6
```

### Postman Collection

_Import Wallet_Collection.postman_collection.json to postman client._

### Code Coverage
```
coverlet .\UnitsTest\bin\Debug\netcoreapp3.1\UnitsTest.dll --target "dotnet" --targetargs "test ./UnitsTest --no-build"
```
### To do

- Authentication
- Api Versionation
- Angular Client
- Configure Docker's Enviroments
- Handler queries with mediator pattern.

### Deployment 📦
```
docker stack deploy  -c docker-compose.prod.yml --with-registry-auth wallet-app
```
### License 📄

This project is licensed under the MIT License - see the LICENSE.md file for details 
