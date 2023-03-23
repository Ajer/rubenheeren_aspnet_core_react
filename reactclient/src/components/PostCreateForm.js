import React,{useState} from 'react';
import Constants from '../Utilities/Constants';

export default function PostCreateForm(props)
{
    
    
    const initialFormData = Object.freeze({
        title:'Fill in title of Post',
        content:'Fill in content for post',
       /* createdTime: new Date()*/
    });

    const [formData,setFormData] = useState(initialFormData);
    
    const handleChange = (e) =>{
       setFormData({
           ...formData,
           [e.target.name]:e.target.value
       });
    };

    
    
    const handleSubmit = (e) =>{
        e.preventDefault();
        const postToCreate ={
            postId: 0,
            title: formData.title,
            content: formData.content,
            createdTime: new Date()
        };
        const url = Constants.API_URL_CREATE_POST;

        fetch(url,{
            method: 'POST',
            headers: {'Content-type':'application/json'},
            body:JSON.stringify(postToCreate)
          })
          
          .then(response =>response.json()).then(respsonseFromServer => {
              console.log(respsonseFromServer);
            }).catch((error)=>{
               console.log(error);
               alert(error);
            });

            props.onPostCreated(postToCreate);
    };

    return(
        <form className="w-100 px-5">
             <h1 className='mt-5'>Create New Post</h1>

             <div className='mt-5'>
                <label className='h3 form-label'>Post title</label>
                <input type='text' className='form-control' value={formData.title} name="title" onChange={handleChange}></input>
             </div>
             <div className='mt-5'>
                <label className='h3 form-label'>Post content</label>
                <input type='text' className='form-control' value={formData.content} name="content" onChange={handleChange}></input>
             </div>
             <button onClick={handleSubmit} className="btn btn-dark btn-lg w-100 mt-5">Submit</button>
             <button onClick={() => props.onPostCreated(null)} className="btn btn-secondary btn-lg w-100 mt-3">Cancel</button>
        </form>
    )
}