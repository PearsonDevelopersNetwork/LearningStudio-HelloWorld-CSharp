# Learning Studio Hello World in C-Sharp

## Prerequisites

### Server Environment 

**Application**

This application assumes you're running an application server (i.e. IIS). 

**.Net Framework**

This application requires ASP.Net 4.0 or greater. 

**Networking**

The LearningStudio APIs have no special firewall or IP address restrictions. Simply ensure your machine or network allows *outbound* connections to api.learningstudio.com. Unless you are on a highly secured corporate network, this is rarely be a problem. Note, the API hostname does not respond to PINGs. 

## LearningStudio API Keys

You will need to register on the Pearson Developers Network and create an application to obtain API keys and sandbox courses. When you create an application, you'll receive an email (within 5 minutes) containing your Application ID, two sandbox courses, and two sandbox users (a teacher and a student). You will also get the campus keys for the Sandbox Campus. 

Please be sure to read the documentation to better understand our data model and authentication scheme. 

 * [How to Get Keys](http://pdn.pearson.com/learningstudio/get-a-key)
 * [About the Sandbox Campus](http://pdn.pearson.com/learningstudio/sandbox-campus)

## Installation

### Application Configuration

Once you obtain your API keys and sandbox credentials (see above), you'll need to add these values to the empty place holders in the `helloworld/web.config` file.

### Build

In Visual Studio Build the project to prepare the application for use.

### Server 

Host the app under IIS with an alias name of "helloworld"

## Operation

Once installed and configured, go to the helloworld folder in your browser, e.g.: 

http://localhost/helloworld/index.html

The index.html page describes how to use the application. 


## License

Copyright (c) 2014 Pearson Education Inc.
Created by Pearson Developer Services

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
