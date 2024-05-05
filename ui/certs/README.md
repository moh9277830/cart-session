# Install SSL Certifcate

## Step 1: Creating an SSL Certificate

To create a valid SSL certificate easily we make use of mkcert tool. Run the following commands to obtain a certificate.

> mkdir -p .certs

Now we have successfully created Certificate Authority (CA) on our machine which enables us to generate certificates for all of our future projects.
From the root of our hello-world project, run the following commands,

> mkcert -key-file ./.certs/key.pem -cert-file ./.certs/cert.pem "localhost"

Now we have a .certs folder with key and cert files inside it.
Please, make sure to add this folder to .gitignore so that we don't accidently commit these files to version control.

## Step 2: Adding HTTP to vue config file

Vue CLI allows us to configure it via config file as follows


```
const fs = require('fs');

module.exports = {
  devServer: {
    open: process.platform === 'darwin',
    host: '0.0.0.0',
    port: 8080,
    https: {
      key: fs.readFileSync('.certs/key.pem'),
      cert: fs.readFileSync('.certs/cert.pem'),
    },
    hotOnly: false,
  },
};
```