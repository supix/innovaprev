const fs = require('fs-extra');
const childProcess = require('child_process');

try {
	fs.removeSync('./dist/');

	console.log('Generating tsoa spec and routes...');
	childProcess.execSync('npx tsoa spec-and-routes', { stdio: 'inherit' });

	console.log('Building TypeScript...');
	childProcess.execSync('tsc --build tsconfig.prod.json', { stdio: 'inherit' });

	console.log('Build completed successfully');
} catch (err) {
	console.error('Build failed:', err.message);
	process.exit(1);
}
