/**
 * Transfusion API
 * API used for the transfusion centers: user administration, reservation management and transfusion auditing.
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { Sex } from './sex';

export interface DonorPersonalInfoDto { 
    readonly id?: number;
    readonly firstName?: string;
    readonly lastName?: string;
    sex?: Sex;
    readonly jmbg?: string;
    readonly state?: string;
    readonly homeAddress?: string;
    readonly city?: string;
    readonly phoneNumber?: string;
    readonly occupation?: string;
    readonly occupationInfo?: string;
}