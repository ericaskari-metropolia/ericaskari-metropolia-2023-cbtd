### Create Migration

```bash
dotnet ef migrations add ProductModelAdded --project DataAccess --context ApplicationDbContext --startup-project CBTD --verbose
```

### Run Migration

```bash
dotnet ef database update --project DataAccess --context ApplicationDbContext --startup-project CBTD --verbose
```

## Create Local SSL

### Generate Authority keys for development

```bash
#mkdir -p ~/dev-ca && openssl genrsa -out ~/dev-ca/certificate-authority.key 2048
#mkdir -p ~/dev-ca && openssl req -x509 -config ssl.cnf -new -nodes -key ~/dev-ca/certificate-authority.key -sha256 -days 1825 -out ~/dev-ca/certificate-authority.pem
```

Add the root certificate to keychain and choose always trust.

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