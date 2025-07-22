# FullStackTest UI

A modern Angular task management application that provides a clean interface for managing tasks through a RESTful API.

## Features

- **Task Viewing**: Fetch and display tasks from a WebAPI
- **Task Creation**: Add new tasks with custom names (up to 10 characters)
- **Task Management**: Toggle task completion status by clicking on checkboxes or task names
- **Real-time Updates**: Dynamic UI updates when tasks are modified
- **Responsive Design**: Works across different screen sizes
- **Comprehensive Testing**: Unit tests with Jest and E2E tests with Cypress
- **Custom Commands**: Reusable Cypress commands for streamlined testing

## Recent Changes and Improvements

### Enhanced Testing Framework
- **Custom Cypress Commands**: Added reusable commands for common task operations (addTask, toggleTask, verifyTaskExists, verifyTaskCompleted)
- **Comprehensive E2E Coverage**: Expanded test suite includes:
  - UI functionality testing
  - API integration testing with proper mocking
  - Responsive design validation
  - Task workflow automation testing
- **Improved Test Organization**: Better structured test files with clear separation of concerns
- **Task Input Validation**: Enhanced testing for task name length restrictions (10-character maximum)

### Code Quality Improvements
- **Better Error Handling**: Improved error handling in task service operations
- **Enhanced User Experience**: Refined task interaction patterns and visual feedback
- **Performance Optimizations**: Optimized API calls and state management

## Prerequisites and Installation

### Step 1: Install Node.js

Node.js is required to run this Angular application. Follow the instructions for your operating system:

#### Windows:
1. Visit the official Node.js website: https://nodejs.org/
2. Download the LTS (Long Term Support) version for Windows
3. Run the downloaded installer (.msi file)
4. Follow the installation wizard (accept default settings)
5. Verify installation by opening Command Prompt or PowerShell and running:
   ```powershell
   node --version
   npm --version
   ```

#### macOS:
1. **Option A - Official Installer:**
   - Visit https://nodejs.org/
   - Download the LTS version for macOS
   - Run the .pkg installer and follow the wizard

2. **Option B - Using Homebrew (recommended):**
   ```bash
   # Install Homebrew if not already installed
   /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
   
   # Install Node.js
   brew install node
   ```

3. Verify installation:
   ```bash
   node --version
   npm --version
   ```

#### Linux (Ubuntu/Debian):
1. **Option A - Using Node Version Manager (recommended):**
   ```bash
   # Install nvm
   curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.0/install.sh | bash
   
   # Restart terminal or run:
   source ~/.bashrc
   
   # Install latest LTS Node.js
   nvm install --lts
   nvm use --lts
   ```

2. **Option B - Using Package Manager:**
   ```bash
   # Update package index
   sudo apt update
   
   # Install Node.js and npm
   sudo apt install nodejs npm
   ```

3. Verify installation:
   ```bash
   node --version
   npm --version
   ```

### Step 2: Install Angular CLI

Angular CLI is a command-line interface tool for Angular development:

```bash
# Install Angular CLI globally
npm install -g @angular/cli

# Verify installation
ng version
```

**Note:** If you encounter permission errors on macOS/Linux, you may need to use `sudo`:
```bash
sudo npm install -g @angular/cli
```

**Alternative for Windows:** If you prefer not to install globally, you can use npx:
```bash
npx @angular/cli version
```

### Step 3: Verify Prerequisites

Ensure you have the correct versions:
- **Node.js**: v18.0.0 or higher
- **npm**: v8.0.0 or higher  
- **Angular CLI**: v19.0.0 or higher

Check your versions:
```bash
node --version    # Should show v18.x.x or higher
npm --version     # Should show v8.x.x or higher
ng version        # Should show Angular CLI v19.x.x or higher
```

## Quick Start

### Installation and Setup

1. **Clone or navigate to the project folder:**
   ```bash
   cd fullstacktest-ui
   ```

2. **Install project dependencies:**
   ```bash
   npm install
   ```
   This command will install all the required packages listed in `package.json`, including:
   - Angular framework (v19.2.0)
   - Development tools and testing frameworks
   - Cypress for E2E testing
   - Jest for unit testing

3. **Start the development server:**
   ```bash
   npm start
   # or alternatively
   ng serve
   ```

4. **Open your browser:**
   Navigate to `http://localhost:4200`

The application will automatically reload when you modify source files.

## Configuration

### API Endpoint Setup
The application connects to a backend API for task management. Update the following files to match your backend configuration:

- **API Service**: Update `src/app/tasks/tasks.service.ts` - modify the `apiUrl` property
- **Proxy Configuration**: Update `proxy.conf.json` for development API proxying

### Environment Configuration
Configure different environments by modifying:
- Development: Default configuration in service files
- Production: Update build configurations in `angular.json`

## Development

### Development server

To start a local development server:

```bash
ng serve
# or
ng serve --open
```

The application will automatically reload when you modify source files. Navigate to `http://localhost:4200/` to view the app.

### Code scaffolding

Generate new Angular components, services, and other artifacts:

```bash
# Generate a new component
ng generate component component-name

# Generate a new service
ng generate service service-name

# Generate a new module
ng generate module module-name
```

For a complete list of available schematics:

```bash
ng generate --help
```

## Building

### Development Build
```bash
ng build
# or
npm run build
```

### Production Build
```bash
ng build --configuration production
```

Build artifacts will be stored in the `dist/` directory.

## Testing

### Unit Tests
The project uses Jest for unit testing:

```bash
# Run tests once
npm test
# or
npm run test

# Run tests in watch mode
npm run test:watch

# Run tests with coverage report
npm run test:coverage
```

### End-to-End (E2E) Tests
The project uses Cypress for end-to-end testing:

```bash
# Open Cypress Test Runner (interactive mode)
npm run cypress:open
# or
npx cypress open

# Run E2E tests in headless mode
npm run cypress:run
# or
npx cypress run

# Run E2E tests in specific browsers
npm run e2e:chrome      # Run in Chrome
npm run e2e:firefox     # Run in Firefox
npm run e2e:headless    # Run headless

# Start dev server and open Cypress simultaneously
npm run e2e:dev
```

#### E2E Test Structure
The E2E tests are organized as follows:

- `cypress/e2e/spec.cy.ts` - Basic application smoke tests
- `cypress/e2e/task-management.cy.ts` - Task creation and completion tests
- `cypress/e2e/api-integration.cy.ts` - API interaction tests with mocking
- `cypress/e2e/responsive.cy.ts` - Responsive design tests across devices
- `cypress/e2e/ui-functionality.cy.ts` - User interface functionality tests
- `cypress/e2e/custom-commands.cy.ts` - Tests using custom Cypress commands for efficient testing

#### Custom Cypress Commands
The project includes custom commands for common task operations, making tests more maintainable and readable:

```typescript
// Add a new task (respects 10-character limit)
cy.addTask('Learn Cy')

// Toggle task completion status
cy.toggleTask('Task name')

// Verify a task exists in the list
cy.verifyTaskExists('Task name')

// Verify task completion status
cy.verifyTaskCompleted('Task name', true)  // true for completed, false for pending
```

These custom commands are defined in `cypress/support/commands.ts` and provide a clean, reusable way to interact with the task management interface during testing.

### Test Coverage
- View unit test coverage: `coverage/lcov-report/index.html`
- E2E test screenshots and videos: `cypress/screenshots/` and `cypress/videos/`

## Project Structure

```
src/
├── app/
│   ├── tasks/              # Task management module
│   │   ├── task.model.ts          # Task data model
│   │   ├── tasks.service.ts       # API service for tasks
│   │   ├── tasks-list.component.* # Task list component
│   │   └── tasks.module.ts        # Tasks module
│   ├── app.component.*     # Root application component
│   ├── app.config.ts       # App configuration
│   └── app.module.ts       # Root module
├── styles.css              # Global styles
├── main.ts                 # Application bootstrap
└── index.html              # Main HTML file
```

## API Integration

The application communicates with a backend API using the following endpoints:

- `GET /api/MyTasks` - Fetch all tasks
- `POST /api/MyTasks` - Create a new task
- `PUT /api/MyTasks` - Update an existing task

### Task Model
```typescript
interface Task {
  id: number;
  name: string;
  isCompleted: boolean;
}
```

## Troubleshooting

### Common Issues and Solutions

#### Node.js Installation Issues:
- **Permission errors on macOS/Linux**: Use `nvm` instead of system package managers
- **Windows PATH issues**: Restart Command Prompt/PowerShell after installation
- **Version conflicts**: Use `nvm` (Node Version Manager) to manage multiple Node.js versions

#### Angular CLI Issues:
- **Command not found**: Ensure Angular CLI is installed globally or use `npx @angular/cli`
- **Version mismatches**: Update Angular CLI with `npm update -g @angular/cli`

#### Development Server Issues:
- **Port 4200 already in use**: Use `ng serve --port 4201` to run on a different port
- **Module not found errors**: Delete `node_modules` folder and run `npm install` again
- **Build errors**: Ensure you have the correct Node.js version (v18+)

#### Testing Issues:
- **Cypress installation problems**: Run `npx cypress install` to reinstall Cypress binary
- **Jest configuration issues**: Ensure `jest.config.js` is properly configured for Angular

## Deployment

## Deployment

### Build for Production
1. **Create a production build:**
   ```bash
   ng build --configuration production
   ```
   This command will:
   - Optimize the code for production
   - Minify CSS and JavaScript files
   - Enable ahead-of-time (AOT) compilation
   - Generate source maps for debugging

2. **Deploy the contents** of `dist/fullstacktest-ui/` to your web server

### Deployment Options

#### Static Web Hosting (Recommended for SPAs):
- **Netlify**: Drag and drop the `dist/fullstacktest-ui` folder
- **Vercel**: Connect your GitHub repository for automatic deployments
- **GitHub Pages**: Use GitHub Actions for automated deployment
- **AWS S3 + CloudFront**: For scalable static hosting

#### Traditional Web Servers:
- **Apache**: Copy files to `htdocs` or `www` directory
- **Nginx**: Copy files to configured web root directory
- **IIS**: Deploy to wwwroot folder

#### Docker Deployment:
```dockerfile
FROM nginx:alpine
COPY dist/fullstacktest-ui /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

### Environment Configuration
Configure different environments by updating:
- Development: Default configuration in service files
- Production: Update API endpoints and build configurations

### Important Notes for Production:
- Ensure your backend API supports CORS for your domain
- Configure proper routing for single-page application (SPA)
- Set up proper error pages (404.html)
- Configure HTTPS for secure connections

## Available Scripts

The following npm scripts are available for development and deployment:

```bash
# Development
npm start              # Start development server
npm run build          # Build for development
npm run watch          # Build and watch for changes

# Testing
npm test               # Run unit tests with Jest
npm run test:watch     # Run tests in watch mode
npm run test:coverage  # Generate test coverage report

# E2E Testing
npm run cypress:open   # Open Cypress test runner
npm run cypress:run    # Run Cypress tests headlessly
npm run e2e:chrome     # Run E2E tests in Chrome
npm run e2e:firefox    # Run E2E tests in Firefox
npm run e2e:headless   # Run E2E tests in headless mode
npm run e2e:dev        # Start dev server and Cypress together

# Production
ng build --configuration production  # Build for production
```


