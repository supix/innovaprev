import dotenv from 'dotenv';
import * as fs from 'fs';

const path = './env/.env.development.local';

if (fs.existsSync(path)) {
  /**
   * Set the env file
   */
  const result = dotenv.config({path});

  if (result.error) {
    throw result.error;
  }

}
