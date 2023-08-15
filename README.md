### Create Migration

```bash
dotnet ef migrations add FreshStart --project DataAccess --context ApplicationDbContext --startup-project CBTD --verbose
```

### Run Migration

```bash
dotnet ef database update --project DataAccess --context ApplicationDbContext --startup-project CBTD --verbose
```

## Steps to run the project locally on MACOS

1. ```bash echo "127.0.0.1       cbtd.localnet" >> /etc/hosts```
2. Create Local SSL Root Authority (Only once) [Go to section](#generate-authority-keys-for-development)
3. Create Domain Certificate (Only once) [Go to section](#generate-domain-certificate)
4. Run Database in the background (Restart is off) (Remove '-d' for logs) ```docker-compose up --build -d```
5. Run the migrations [Go to section](#run-migration)
6. Start the project from IDE or run following ```bash dotnet run --project CBTD```
7. Open https://cbtd.localnet

### Generate Authority keys for development

```bash
#mkdir -p ~/dev-ca && openssl genrsa -out ~/dev-ca/certificate-authority.key 2048
#mkdir -p ~/dev-ca && openssl req -x509 -config ssl.cnf -new -nodes -key ~/dev-ca/certificate-authority.key -sha256 -days 1825 -out ~/dev-ca/certificate-authority.pem
```

Double click on certificate and add it to the keychain and choose always trust.

### Generate Domain Certificate

```bash
openssl genrsa -out ssl.key 2048

openssl req -new -config ssl.cnf -key ssl.key -out ssl.csr

openssl x509 \
-req \
-in ssl.csr \
-CA ~/dev-ca/certificate-authority.pem \
-CAkey ~/dev-ca/certificate-authority.key \
-CAcreateserial \
-out ssl.crt \
-days 825 \
-sha256 \
-extfile ssl.ext
```