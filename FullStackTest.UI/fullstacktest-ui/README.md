# FullStackTest UI

A modern Angular task management application that provides a clean interface for managing tasks through a RESTful API.

## Features

- **Task Viewing**: Fetch and display tasks from a WebAPI
- **Task Creation**: Add new tasks with custom names
- **Task Management**: Toggle task completion status by clicking on checkboxes or task names
- **Real-time Updates**: Dynamic UI updates when tasks are modified
- **Responsive Design**: Works across different screen sizes

## Quick Start

### Prerequisites
- Node.js (v18 or higher)
- npm or yarn package manager
- Angular CLI (optional but recommended)

### Installation

1. **Navigate to the project folder:**
   ```bash
   cd fullstacktest-ui
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Start the development server:**
   ```bash
   npm start
   # or
   ng serve
   ```

4. **Open your browser:**
   Navigate to `http://localhost:4200`

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

### Test Coverage
View the coverage report by opening `coverage/lcov-report/index.html` in your browser after running tests with coverage.

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

## Deployment

### Build for Production
1. Create a production build:
   ```bash
   ng build --configuration production
   ```

2. Deploy the contents of `dist/fullstacktest-ui/` to your web server

### Environment Variables
Configure different environments by updating:
- `src/environments/environment.ts` (development)
- `src/environments/environment.prod.ts` (production)


