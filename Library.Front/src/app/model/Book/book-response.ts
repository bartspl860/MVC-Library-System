import { AuthorResponse } from "../Author/author-response";
import { PublishingHouseResponse } from "../PublishingHouse/publishing-house-response";

export interface BookResponse{
    title: string;
    authors: AuthorResponse[];
    publishingHouse: PublishingHouseResponse;
}