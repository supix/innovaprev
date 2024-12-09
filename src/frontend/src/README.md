
# Innova-FE

**Innova-FE** is a web application designed for calculating, customizing, and downloading estimates. Built with **Angular 17** and styled using **Bootstrap**, the app offers a streamlined user experience for managing personal, billing, and window estimation data.

## Features

- **Dynamic Header**:
  - Buttons for price calculation and PDF download.
  - Displays the calculated price dynamically.
- **Multi-Tab Form**:
  - Separate sections for personal, billing, and window estimate data.
  - Dynamic row management with robust validation.
- **API Integration**:
  - Supports price calculation and timestamped PDF generation.
  - Configurable base URL for flexible deployment environments.
- **Error Handling**:
  - Global error interceptor with dynamic modal for error messages.
- **Responsive Design**:
  - Table layout for larger screens; card-based layout for smaller screens.

## Folder Structure

```plaintext
src/
├── app/
│   ├── components/        # UI components and form logic
│   ├── interceptors/      # HTTP interceptors
│   ├── models/            # Data models for API
│   ├── services/          # API and configuration services
│   ├── app.component.*    # Main app component
│   ├── app.config.*       # Environment-specific configurations
│   ├── app.routes.ts      # App routing
├── assets/                # Static assets
├── environments/          # Environment variables
├── styles.scss            # Global styles
└── index.html             # Entry point
```

## Improvements
- Enhance accessibility with ARIA roles and better keyboard navigation.

## Getting Started

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/supix/innovaprev.git
   cd src/frontend/src/src
   ```
2. Install dependencies:
   ```bash
   npm install
   ```

### Development
1. Run the app:
   ```bash
   npm start
   ```
2. Access the app at `http://localhost:4200`.

### Build
Generate a production build:
```bash
npm run build
```

### Docker
1. Build the Docker image from project root:
   ```bash
   docker build -t my-angular-app -f src/frontend/docker/Dockerfile.prod .
   ```
2. Run the container:
   ```bash
   docker run -d -p 8080:80 innova-fe
   ```
3. Access at `http://localhost:8080`.

## License
This project is licensed under the MIT License.
