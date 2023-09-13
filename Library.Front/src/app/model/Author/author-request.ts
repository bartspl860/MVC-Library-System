import { AuthorResponse } from "./author-response";

export class AuthorRequest{
    constructor(private name: string, private surname: string, private dateOfBirth: Date){
    }

    static FromAuthorResponse(res: AuthorResponse) : AuthorRequest{
        let authorReq = new AuthorRequest(
            res.name,
            res.surname,
            res.dateOfBirth
        );
        return authorReq;
    }
}