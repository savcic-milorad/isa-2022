export * from './donor.service';
import { DonorService } from './donor.service';
export * from './identity.service';
import { IdentityService } from './identity.service';
export const APIS = [DonorService, IdentityService];
